import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { TextField, Button, Container, Typography } from '@mui/material';

import { usePermissions } from '../context/permissions/permissionContext';

const PermissionForm = () => {
  const { state, addPermission, editPermission } = usePermissions();
  const navigate = useNavigate();
  const { id } = useParams<{ id?: string }>();

  const isEditing = Boolean(id);
  const existingPermission = state.permissions.find(p => p.id === Number(id));

  const [form, setForm] = useState({
    employeeName: '',
    employeeLastName: '',
    description: ''
  });

  useEffect(() => {
    if (isEditing && existingPermission) {
      setForm({
        employeeName: existingPermission.employeeName,
        employeeLastName: existingPermission.employeeLastName,
        description: existingPermission.description
      });
    }
    
  }, [id, existingPermission, isEditing]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (isEditing) {
      await editPermission({ id: Number(id), ...form });
    } else {
      await addPermission(form);
    }
    navigate('/');
  };

  return (
    <Container>
      <Typography variant="h5">{isEditing ? 'Editar Permiso' : 'Nuevo Permiso'}</Typography>
      <form onSubmit={handleSubmit}>
        <TextField
          fullWidth
          label="Nombre"
          name="employeeName"
          value={form.employeeName}
          onChange={handleChange}
          margin="normal"
          required
        />
        <TextField
          fullWidth
          label="Apellido"
          name="employeeLastName"
          value={form.employeeLastName}
          onChange={handleChange}
          margin="normal"
          required
        />
        <TextField
          fullWidth
          label="Descripción"
          name="description"
          value={form.description}
          onChange={handleChange}
          margin="normal"
          required
        />
        <Button type="submit" variant="contained" color="primary">
          {isEditing ? 'Actualizar' : 'Crear'}
        </Button>
      </form>
    </Container>
  );
};

export default PermissionForm;
