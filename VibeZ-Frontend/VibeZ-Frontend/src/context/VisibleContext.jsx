import React, { createContext, useContext, useState } from 'react';

const ListVisibilityContext = createContext();

export function useListVisibility() {
  return useContext(ListVisibilityContext);
}

export function ListVisibilityProvider({ children }) {
  const [isListVisible, setIsListVisible] = useState(false);

  return (
    <ListVisibilityContext.Provider value={{ isListVisible, setIsListVisible}}>
      {children}
    </ListVisibilityContext.Provider>
  );
}

