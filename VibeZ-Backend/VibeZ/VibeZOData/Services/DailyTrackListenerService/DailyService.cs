using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace VibeZOData.Services.DailyTrackListenerService
{
    public class DailyService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DailyService> _logger;

        public DailyService(IServiceProvider serviceProvider, ILogger<DailyService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("DailyService started executing.");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("DailyService is waiting until midnight.");
                // Đợi đến 00:00 của ngày tiếp theo để đảm bảo chạy một lần vào đầu ngày
                var midnight = DateTime.Today.AddDays(1);
                var delayUntilMidnight = TimeSpan.FromSeconds(1800);
                await Task.Delay(delayUntilMidnight, stoppingToken);

                _logger.LogInformation("DailyService started processing tasks at midnight.");

                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<VibeZDbContext>();
                    var today = DateOnly.FromDateTime(DateTime.Today);
                    var yesterday = DateOnly.FromDateTime(DateTime.Today.AddDays(-1));

                    // Kiểm tra nếu đã có bản ghi của ngày hôm nay
                    bool exists = await context.TrackListeners.AnyAsync(tl => tl.Date == today, stoppingToken);
                    bool exists2 = await context.ArtistFollows.AnyAsync(tl => tl.Date == today, stoppingToken);

                    // Tạo danh sách TrackListeners cho ngày hôm nay
                    var trackListeners = await context.Tracks
                          .Select(track => new TrackListener
                          {
                              TrackId = track.TrackId,
                              Date = today,
                              Listener = Math.Max(0, (context.Tracks
                                                 .Where(t => t.TrackId == track.TrackId && t.UpdateDate == today)
                                                 .Select(t => t.Listener)
                                                 .FirstOrDefault()
                                             -
                                             context.TrackListeners
                                                 .Where(tl => tl.TrackId == track.TrackId && tl.Date == yesterday)
                                                 .Select(tl => tl.Listener)
                                                 .FirstOrDefault())),
                              SavedTrack = Math.Max(0, (context.TrackPlayLists
                                                      .Where(x => x.Track.ArtistId == track.ArtistId && x.CreateDate == today)
                                                      .Count()
                                                 -
                                                 context.TrackListeners
                                                     .Where(tl => tl.Track.ArtistId == track.ArtistId && tl.Date == yesterday)
                                                     .Select(tl => tl.SavedTrack)
                                                     .FirstOrDefault()))
                          })
                          .ToListAsync(stoppingToken);

                    // Tạo danh sách ArtistFollows cho ngày hôm nay
                    var artistFollow = await context.Follows
                        .Select(fl => new ArtistFollow
                        {
                            ArtistId = fl.ArtistId,
                            Date = today,
                            TotalFollow = Math.Max(0, (context.Follows
                                                     .Where(t => t.ArtistId == fl.ArtistId && t.CreateDate == today && t.IsFollow == true)
                                                     .Count()
                                                 -
                                                 context.ArtistFollows
                                                     .Where(t => t.ArtistId == fl.ArtistId && t.Date == yesterday)
                                                     .Select(tl => tl.TotalFollow)
                                                     .FirstOrDefault())),
                            TotalUnfollow = Math.Max(0, (context.Follows
                                                     .Where(t => t.ArtistId == fl.ArtistId && t.CreateDate == today && t.IsFollow == false)
                                                     .Count()
                                                 -
                                                 context.ArtistFollows
                                                     .Where(t => t.ArtistId == fl.ArtistId && t.Date == yesterday)
                                                     .Select(tl => tl.TotalUnfollow)
                                                     .FirstOrDefault()))
                        })
                        .ToListAsync(stoppingToken);

                    // Thêm hoặc cập nhật TrackListeners cho ngày hôm nay
                    if (!exists)
                    {
                        _logger.LogInformation("Adding TrackListeners for today.");
                        await context.TrackListeners.AddRangeAsync(trackListeners, stoppingToken);
                    }
                    else
                    {
                        _logger.LogInformation("Updating TrackListeners for today.");
                        foreach (var trackListener in trackListeners)
                        {
                            var existingTrackListener = await context.TrackListeners
                                .FirstOrDefaultAsync(tl => tl.TrackId == trackListener.TrackId && tl.Date == today, stoppingToken);

                            if (existingTrackListener != null)
                            {
                                existingTrackListener.Listener = trackListener.Listener;
                                existingTrackListener.SavedTrack = trackListener.SavedTrack;
                            }
                            else
                            {
                                await context.TrackListeners.AddAsync(trackListener, stoppingToken);
                            }
                        }
                    }
                    await context.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("TrackListeners for today processed successfully.");

                    // Thêm hoặc cập nhật ArtistFollows cho ngày hôm nay
                    if (!exists2)
                    {
                        _logger.LogInformation("Adding ArtistFollows for today.");
                        await context.ArtistFollows.AddRangeAsync(artistFollow, stoppingToken);
                    }
                    else
                    {
                        _logger.LogInformation("Updating ArtistFollows for today.");
                        foreach (var artistFollowRecord in artistFollow)
                        {
                            var existingArtistFollow = await context.ArtistFollows
                                .FirstOrDefaultAsync(af => af.ArtistId == artistFollowRecord.ArtistId && af.Date == today, stoppingToken);

                            if (existingArtistFollow != null)
                            {
                                existingArtistFollow.TotalFollow = artistFollowRecord.TotalFollow;
                                existingArtistFollow.TotalUnfollow = artistFollowRecord.TotalUnfollow;
                            }
                            else
                            {
                                await context.ArtistFollows.AddAsync(artistFollowRecord, stoppingToken);
                            }
                        }
                    }
                    await context.SaveChangesAsync(stoppingToken);
                    _logger.LogInformation("ArtistFollows for today processed successfully.");
                }

                _logger.LogInformation("DailyService stopped executing.");

            }
            _logger.LogInformation("DailyService stopped executing.");
        }
    }
}
