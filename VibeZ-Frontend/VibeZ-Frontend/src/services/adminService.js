import axios from 'axios';

const API_URL = 'https://localhost:7241/api/Admin';

const getTotalData = async () => {
    try {
        const response = await axios.get(`${API_URL}/total-data`);
        console.log(response.data); 
        return response.data;
    } catch (error) {
        console.error("Error fetching data:", error.message || error);
        throw new Error(
            "Failed to fetch data: " +
            (error.response?.data?.message || error.message)
        );
    }
};

const getArtistData = async () => {
    try {
        const response = await axios.get(`${API_URL}/artist-data`);
        console.log(response.data); 
        return response.data;
    } catch (error) {
        console.error("Error fetching artist admin data:", error.message || error);
        throw new Error(
            "Failed to fetch artist admin data: " +
            (error.response?.data?.message || error.message)
        );
    }
};

const getAdminHome = async () => {
    try {
        const response = await axios.get(`${API_URL}/admin-home`);
        console.log(response.data); 
        return response.data;
    } catch (error) {
        console.error("Error fetching admin home data:", error.message || error);
        throw new Error(
            "Failed to fetch admin home data: " +
            (error.response?.data?.message || error.message)
        );
    }
}

const getAdminBan = async() => {
    try {
        const response = await axios.get(`${API_URL}/admin-ban`);
        console.log(response.data);
        return response.data;
    } catch (error) {
        console.error("Error fetching admin ban data:", error.message || error);
        throw new Error(
            "Failed to fetch admin ban data: " +
            (error.response?.data?.message || error.message)
        );
    }
}

const getArtistApproval = async() => {
    try {
        const response = await axios.get(`https://localhost:7241/api/ArtistPending`);
        return response.data;
    } catch (error) {
        console.error("Error fetching artist pending data:", error.message || error);
        throw new Error(
            "Failed to fetch artist pending data: " +
            (error.response?.data?.message || error.message)
        );
    }
}
const DeleteApproval = async(id) => {
    try {
        const response = await axios.delete(`https://localhost:7241/api/ArtistPending/${id}`);
        return response.status == 204 ? "Ok" : null;
    } catch (error) {
        console.error("Error fetching artist pending data:", error.message || error);
        throw new Error(
            "Failed to fetch artist pending data: " +
            (error.response?.data?.message || error.message)
        );
    }
}
const UpdateUserRole = async(id) => {
    try {
        const response = await axios.put(`https://localhost:7241/odata/User/role/${id}`);
        return response.status == 200 ? "Ok" : null;
    } catch (error) {
        console.error("Error fetching artist pending data:", error.message || error);
        throw new Error(
            "Failed to fetch artist pending data: " +
            (error.response?.data?.message || error.message)
        );
    }
}
const UpdateArtist = async (name, genre, image, imgBackground, nation, email, userId) => {
    try {
        const formData = new FormData();
        formData.append("name", name);
        formData.append("genre", genre);
        formData.append("image", image);
        formData.append("imgBackground", imgBackground);
        formData.append("nation", nation);
        formData.append("email", email);
        formData.append("userId", userId);


        const response = await axios.post(`https://localhost:7241/api/Artist/VerifyArtist`, formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });
        return response.data;
    } catch (error) {
        console.error("Error creating artist:", error.message || error);
        throw new Error(
            "Failed to create artist: " +
            (error.response?.data?.message || error.message)
        );
    }
};
const updateTrackToVerify = async (trackName, lyrics, genre, hour, minute, section, artistId, path, image, trackLRC) => {
    try {
        const formData = new FormData();
        formData.append("trackName", trackName);
        formData.append("lyrics", lyrics);
        formData.append("genre", genre);
        formData.append("hour", hour);
        formData.append("minute", minute);
        formData.append("section", section);
        formData.append("artistId", artistId);
        formData.append("path", path);
        formData.append("image", image);
        formData.append("trackLRC", trackLRC);

        const response = await axios.post(`https://localhost:7241/odata/Track/updateSong`, formData, {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        });

        console.log("Track updated successfully:", response.data);
        return response.data;
    } catch (error) {
        console.error("Error updating track:", error.message || error);
        throw new Error(
            "Failed to update track: " +
            (error.response?.data?.message || error.message)
        );
    }
};

const getAdminApproval = async() => {
    try {
        const response = await axios.get(`${API_URL}/admin-approval`);
        console.log(response.data);
        return response.data;
    } catch (error) {
        console.error("Error fetching admin approval data:", error.message || error);
        throw new Error(
            "Failed to fetch admin approval data: " +
            (error.response?.data?.message || error.message)
        );
    }
}
export { getTotalData, getArtistData, getAdminHome, getAdminBan, getAdminApproval, DeleteApproval, UpdateArtist, updateTrackToVerify, getArtistApproval, UpdateUserRole };
