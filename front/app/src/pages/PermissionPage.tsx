import { useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';

import { Container, Typography, Button } from '@mui/material';

import DataTable from '../components/DataTable';
import { usePermissions } from '../context/permissions/permissionContext';

const PermissionsPage = () => {
  const { state, fetchPermissions } = usePermissions();
  const location = useLocation();

  useEffect(() => {
    fetchPermissions(state.currentPage);
  }, [location.pathname]);

  const columns = [
    { key: 'id', label: 'ID' },
    { key: 'employeeName', label: 'Nombre' },
    { key: 'employeeLastName', label: 'Apellido' },
    { key: 'description', label: 'Descripción' },
    { key: 'permissionDateOnUtc', label: 'Fecha' }
  ];

  const formattedPermissions = state.permissions.map((perm) => ({
    ...perm,
    permissionDateOnUtc: new Date(perm.permissionDateOnUtc).toLocaleDateString()
  }));

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Permisos
      </Typography>
      <Button variant="contained" color="primary" component={Link} to="/new">
        Nuevo Permiso
      </Button>

      <DataTable
        columns={columns}
        data={formattedPermissions}
        loading={state.loading}
        error={state.error ?? ''}
        totalPages={state.totalPages}
        currentPage={state.currentPage}
        onPageChange={fetchPermissions}
        retry={() => fetchPermissions(state.currentPage)}
      />
    </Container>
  );
};

export default PermissionsPage;
