import { Box, Typography, Button } from "@mui/material";
import ErrorOutlineIcon from "@mui/icons-material/ErrorOutline";

const CollectionErrorMessage = ({ message, onRetry }: { message: string; onRetry?: () => void }) => (
  <Box
    display="flex"
    flexDirection="column"
    alignItems="center"
    justifyContent="center"
    height="250px"
    textAlign="center"
    sx={{ color: "error.main" }}
  >
    <ErrorOutlineIcon sx={{ fontSize: 60, mb: 1 }} />
    <Typography variant="h6">Ocurrió un error</Typography>
    <Typography variant="body2" sx={{ mb: 2 }}>
      {message}
    </Typography>
    {onRetry && (
      <Button variant="contained" color="error" onClick={onRetry}>
        Reintentar
      </Button>
    )}
  </Box>
);

export default CollectionErrorMessage;
