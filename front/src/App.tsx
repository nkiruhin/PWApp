import React from 'react';
import './App.css';
import MenuAppBar from './Components/MenuAppBar';
import { MainPage } from './Components/MainPage';
import { BrowserRouter } from 'react-router-dom';
import AuthSwitch from './Components/AuthSwitch';
import { AppContextProvider } from './Components/Context/AppContext';



const App: React.FC = () => {

    if (localStorage.ExpirationTime > Date.now()) {
        localStorage.clear();
    }
    const auth = localStorage?.getItem('Token') !== null;
    
  return (
      <BrowserRouter>
          <div className="App">
              <MenuAppBar auth={auth} />
              {auth ?
                  <AppContextProvider>
                      <MainPage />
                  </AppContextProvider>
                  : <AuthSwitch />}
           </div>
      </BrowserRouter>
  );
}
export default App;