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
  Button
} from '@mui/material'
import { Person, ArrowBack } from '@mui/icons-material'
import { Link } from 'react-router-dom'

const GET_AUTHORS = gql`
  query GetAuthors {
    authors { id fullName }
  }
`

export function AdminAuthorsPage() {
  const { data, loading, error } = useQuery(GET_AUTHORS)
  
  return (
    <Box>
      <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 2 }}>
        <Typography variant="h4" component="h1">
          Manage Authors
        </Typography>
        <Button
          component={Link}
          to="/admin"
          variant="outlined"
          startIcon={<ArrowBack />}
        >
          Back to Dashboard
        </Button>
      </Box>
      
      <Paper sx={{ p: 3 }}>
        <Typography variant="h6" gutterBottom>
          All Authors
        </Typography>
        
        {loading && (
          <Box display="flex" justifyContent="center" p={3}>
            <CircularProgress />
          </Box>
        )}
        
        {error && <Alert severity="error">{error.message}</Alert>}
        
        {data?.authors && (
          <List>
            {data.authors.map((author: any, index: number) => (
              <Box key={author.id}>
                <ListItem>
                  <Avatar sx={{ mr: 2 }}>
                    <Person />
                  </Avatar>
                  <ListItemText
                    primary={author.fullName}
                    secondary={`Author ID: ${author.id}`}
                  />
                </ListItem>
                {index < data.authors.length - 1 && <Divider />}
              </Box>
            ))}
          </List>
        )}
      </Paper>
    </Box>
  )
}


