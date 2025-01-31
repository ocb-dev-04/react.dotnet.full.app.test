import React, { createContext, useReducer, useContext } from 'react';
import { useSnackbar } from 'notistack';

import { PERMISSION_ACTIONS } from './actionsTypes';
import { PermissionGet, PermissionCreate, PermissionUpdate } from '../../models/permissionModels';
import { getPermissions, createPermission, updatePermission } from '../../services/permissionService';


interface PermissionState {
  permissions: PermissionGet[];
  loading: boolean;
  error: string | null;
  currentPage: number;
  totalPages: number;
  totalItems: number;
}

type Action =
  | { type: typeof PERMISSION_ACTIONS.FETCH_START }
  | { type: typeof PERMISSION_ACTIONS.FETCH_SUCCESS; payload: { data: PermissionGet[]; totalItems: number; totalPages: number, currentPage: number } }
  | { type: typeof PERMISSION_ACTIONS.FETCH_ERROR; payload: string }

  | { type: typeof PERMISSION_ACTIONS.ADD_PERMISSION; payload: PermissionGet }
  | { type: typeof PERMISSION_ACTIONS.ADD_ERROR; payload: string }

  | { type: typeof PERMISSION_ACTIONS.UPDATE_PERMISSION; payload: PermissionGet }
  | { type: typeof PERMISSION_ACTIONS.UPDATE_ERROR; payload: string };

const PermissionContext = createContext<{
  state: PermissionState;
  addPermission: (data: PermissionCreate) => Promise<void>;
  editPermission: (data: PermissionUpdate) => Promise<void>;
  fetchPermissions: (page: number) => Promise<void>;
} | undefined>(undefined);

const reducer = (state: PermissionState, action: Action): PermissionState => {
  switch (action.type) {
    case PERMISSION_ACTIONS.FETCH_START:
      return { ...state, loading: true };
    case PERMISSION_ACTIONS.FETCH_SUCCESS:
      return {
        ...state,
        permissions: action.payload.data,
        totalItems: action.payload.totalItems,
        totalPages: action.payload.totalPages,
        currentPage: action.payload.currentPage,
        loading: false,
        error: null,
      };
    case PERMISSION_ACTIONS.FETCH_ERROR:
    case PERMISSION_ACTIONS.ADD_ERROR:
    case PERMISSION_ACTIONS.UPDATE_ERROR:
      return { ...state, loading: false, error: action.payload };
    case PERMISSION_ACTIONS.ADD_PERMISSION:
      return { ...state, permissions: [...state.permissions, action.payload] };
    case PERMISSION_ACTIONS.UPDATE_PERMISSION:
      return {
        ...state,
        permissions: state.permissions.map((perm) =>
          perm.id === action.payload.id ? action.payload : perm
        ),
      };
    default:
      return state;
  }
};

export const PermissionProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, {
    permissions: [],
    loading: false,
    error: null,
    currentPage: 1,
    totalPages: 0,
    totalItems: 0,
  });

  const { enqueueSnackbar } = useSnackbar();

  const fetchPermissions = async (page: number) => {
    dispatch({ type: PERMISSION_ACTIONS.FETCH_START });
    try {
      const { data, totalItems, totalPages } = await getPermissions(page);

      dispatch({
        type: PERMISSION_ACTIONS.FETCH_SUCCESS,
        payload: { data, totalItems, totalPages, currentPage: page },
      });
    } catch (error) {
      dispatch({ type: PERMISSION_ACTIONS.FETCH_ERROR, payload: 'Error fetching permissions' });
      enqueueSnackbar('Error al acceder al listado de permisos', { variant: 'error' });
    }
  };

  const addPermission = async (data: PermissionCreate) => {
    try {
      const newPermission = await createPermission(data);
      dispatch({ type: PERMISSION_ACTIONS.ADD_PERMISSION, payload: newPermission });

      enqueueSnackbar('Permiso creado con éxito', { variant: 'success' });
    } catch (error) {
      dispatch({ type: PERMISSION_ACTIONS.ADD_ERROR, payload: 'Error al crear permiso' });
      enqueueSnackbar('Error al crear permiso', { variant: 'error' });
    }
  };

  const editPermission = async (data: PermissionUpdate) => {
    try {
      const updatedPermission = await updatePermission(data);
      dispatch({ type: PERMISSION_ACTIONS.UPDATE_PERMISSION, payload: updatedPermission });

      enqueueSnackbar('Permiso actualizado con éxito', { variant: 'success' });
    } catch (error) {
      dispatch({ type: PERMISSION_ACTIONS.UPDATE_ERROR, payload: 'Error al actualizar permiso' });
      enqueueSnackbar('Error al actualizar permiso', { variant: 'error' });
    }
  };

  return (
    <PermissionContext.Provider value={{ state, addPermission, editPermission, fetchPermissions }}>
      {children}
    </PermissionContext.Provider>
  );
};

export const usePermissions = () => {
  const context = useContext(PermissionContext);
  if (!context) {
    throw new Error('usePermissions must be used within a PermissionProvider');
  }
  return context;
};
