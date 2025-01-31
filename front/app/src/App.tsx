import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { PermissionProvider } from './context/permissions/permissionContext';

import { SnackbarProvider } from 'notistack';

import PermissionsPage from './pages/PermissionPage';
import PermissionFormPage from './pages/PermissionFormPage';

const App = () => {
  return (
    <SnackbarProvider maxSnack={3} autoHideDuration={3000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }}>
      <PermissionProvider>
        <Router>
          <Routes>
            <Route path="/" element={<PermissionsPage />} />
            <Route path="/new" element={<PermissionFormPage />} />
            <Route path="/edit/:id" element={<PermissionFormPage />} />
          </Routes>
        </Router>
      </PermissionProvider>
    </SnackbarProvider>
  );
};

export default App;
