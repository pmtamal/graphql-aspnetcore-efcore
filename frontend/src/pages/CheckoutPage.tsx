import { 
  Box, 
  Typography, 
  Paper, 
  Button
} from '@mui/material'
import { Payment } from '@mui/icons-material'

export function CheckoutPage() {
  return (
    <Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Checkout
      </Typography>
      
      <Paper sx={{ p: 3 }}>
        <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', py: 4 }}>
          <Payment sx={{ fontSize: 64, color: 'text.secondary', mb: 2 }} />
          <Typography variant="h6" color="text.secondary" gutterBottom>
            Checkout Coming Soon
          </Typography>
          <Typography variant="body2" color="text.secondary" align="center" sx={{ mb: 3 }}>
            Checkout functionality will be added after inventory management is implemented.
          </Typography>
          <Button variant="contained" size="large">
            Back to Cart
          </Button>
        </Box>
      </Paper>
    </Box>
  )
}


