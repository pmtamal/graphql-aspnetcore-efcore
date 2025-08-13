import { 
  Box, 
  Typography, 
  Paper, 
  Button
} from '@mui/material'
import { ShoppingCart } from '@mui/icons-material'

export function CartPage() {
  return (
    <Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Shopping Cart
      </Typography>
      
      <Paper sx={{ p: 3 }}>
        <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', py: 4 }}>
          <ShoppingCart sx={{ fontSize: 64, color: 'text.secondary', mb: 2 }} />
          <Typography variant="h6" color="text.secondary" gutterBottom>
            Your cart is empty
          </Typography>
          <Typography variant="body2" color="text.secondary" align="center" sx={{ mb: 3 }}>
            Cart functionality will be implemented with inventory management later.
          </Typography>
          <Button variant="contained" size="large">
            Continue Shopping
          </Button>
        </Box>
      </Paper>
    </Box>
  )
}


