import { Box, Typography } from '@mui/material';
import InboxIcon from '@mui/icons-material/Inbox';

const EmptyCollectionMessage = () => (
  <Box 
    display="flex" 
    flexDirection="column" 
    alignItems="center" 
    justifyContent="center" 
    height="250px" 
    textAlign="center"
  >
    <InboxIcon color="disabled" sx={{ fontSize: 60, mb: 1 }} />
    <Typography variant="h6" color="textSecondary">
      No hay datos disponibles
    </Typography>
    <Typography variant="body2" color="textSecondary">
      Agrega nuevos registros para verlos aquí.
    </Typography>
  </Box>
);

export default EmptyCollectionMessage;
