import { gql, useQuery } from '@apollo/client'
import { 
  Box, 
  Typography, 
  Paper, 
  CircularProgress, 
  Alert,
  List,
  ListItem,
  ListItemText,
  Divider,
  Avatar,
  Chip
} from '@mui/material'
import { Category } from '@mui/icons-material'

const GET_CATEGORIES = gql`
  query GetCategories {
    categories { id name }
  }
`

export function AdminCategoriesPage() {
  const { data, loading, error } = useQuery(GET_CATEGORIES)
  
  return (
    <Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Manage Categories
      </Typography>
      
      <Paper sx={{ p: 3 }}>
        <Typography variant="h6" gutterBottom>
          All Categories
        </Typography>
        
        {loading && (
          <Box display="flex" justifyContent="center" p={3}>
            <CircularProgress />
          </Box>
        )}
        
        {error && <Alert severity="error">{error.message}</Alert>}
        
        {data?.categories && (
          <List>
            {data.categories.map((category: any, index: number) => (
              <Box key={category.id}>
                <ListItem>
                  <Avatar sx={{ mr: 2, bgcolor: 'primary.main' }}>
                    <Category />
                  </Avatar>
                  <ListItemText
                    primary={category.name}
                    secondary={`Category ID: ${category.id}`}
                  />
                  <Chip label={category.name} color="primary" variant="outlined" />
                </ListItem>
                {index < data.categories.length - 1 && <Divider />}
              </Box>
            ))}
          </List>
        )}
      </Paper>
    </Box>
  )
}


