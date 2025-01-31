import { axiosClient } from "../config/http_client";

import { API_URLS } from "../constants/apiUrls";
import { PaginatedCollection } from "../models/common/paginatedCollection";
import {
  PermissionGet,
  PermissionCreate,
  PermissionUpdate,
} from "../models/permissionModels";

export const getPermissions = async (
  page: number
): Promise<PaginatedCollection<PermissionGet>> => {
  const response = await axiosClient.get(
    `${API_URLS.GET_PERMISSIONS}?pageNumber=${page}`
  );
  return response.data;
};

export const createPermission = async (
  data: PermissionCreate
): Promise<PermissionGet> => {
  const response = await axiosClient.post(API_URLS.REQUEST_PERMISSION, data);
  return response.data;
};

export const updatePermission = async (
  data: PermissionUpdate
): Promise<PermissionGet> => {
  const response = await axiosClient.patch(
    API_URLS.UPDATE_PERMISSION(data.id),
    data
  );
  return response.data;
};
