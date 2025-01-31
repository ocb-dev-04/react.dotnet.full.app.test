import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  CircularProgress,
  Pagination,
  IconButton
} from '@mui/material';
import { Edit } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';

import EmptyCollectionMessage from './EmptyCollectionMessage';
import CollectionErrorMessage from './CollectionErrorMessage';

interface Column {
  key: string;
  label: string;
}

interface DataTableProps {
  columns: Column[];
  data: any[];
  loading: boolean;
  error?: string;
  totalPages?: number;
  currentPage?: number;
  onPageChange?: (page: number) => void;
  retry?: () => void;
}

const DataTable = ({ columns, data, loading, error, totalPages, currentPage, onPageChange, retry }: DataTableProps) => {
  const navigate = useNavigate();
  
  return (
    <>
      {loading ? (
        <CircularProgress style={{ display: 'block', margin: '20px auto' }} />
      ) : error ? (
        <CollectionErrorMessage message={error} onRetry={retry}/>
      ) : data.length === 0 ? (
        <EmptyCollectionMessage />
      ) : (
        <TableContainer component={Paper} style={{ marginTop: '20px' }}>
          <Table>
            <TableHead>
              <TableRow>
                {columns.map((col) => (
                  <TableCell key={col.key}>{col.label}</TableCell>
                ))}
                <TableCell>Acciones</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {data.map((row, index) => (
                <TableRow key={index}>
                  {columns.map((col) => (
                    <TableCell key={col.key}>{row[col.key]}</TableCell>
                  ))}
                  <TableCell>
                    <IconButton color="primary" onClick={() => navigate(`/edit/${row.id}`)}>
                      <Edit />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}

      {totalPages && currentPage && onPageChange && (
        <Pagination
          count={totalPages}
          page={currentPage}
          onChange={(_, page) => onPageChange(page)}
          color="primary"
          style={{ marginTop: '20px', display: 'flex', justifyContent: 'center' }}
        />
      )}
    </>
  );
};

export default DataTable;
