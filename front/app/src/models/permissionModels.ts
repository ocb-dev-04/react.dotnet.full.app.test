export interface PermissionGet {
    id: number;
    employeeName: string;
    employeeLastName: string;
    description: string;
    permissionDateOnUtc: string;
  }
  
  export interface PermissionCreate {
    employeeName: string;
    employeeLastName: string;
    description: string;
  }
  
  export interface PermissionUpdate {
    id: number;
    employeeName: string;
    employeeLastName: string;
    description: string;
  }
  