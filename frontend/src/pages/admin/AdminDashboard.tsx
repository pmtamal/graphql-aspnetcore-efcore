import { 
  Box, 
  Typography, 
  Card, 
  CardContent, 
  CardActionArea,
  Paper
} from '@mui/material'
import { 
  Book, 
  Person, 
  Category,
  Dashboard
} from '@mui/icons-material'
import { Link } from 'react-router-dom'

export function AdminDashboard() {

  const adminItems = [
    {
      title: 'Manage Books',
      description: 'Add, edit, and manage book inventory',
      icon: <Book sx={{ fontSize: 40 }} />,
      path: '/admin/books',
      color: '#1976d2'
    },
    {
      title: 'Manage Authors',
      description: 'Add and manage book authors',
      icon: <Person sx={{ fontSize: 40 }} />,
      path: '/admin/authors',
      color: '#388e3c'
    },
    {
      title: 'Manage Categories',
      description: 'Organize books by categories',
      icon: <Category sx={{ fontSize: 40 }} />,
      path: '/admin/categories',
      color: '#f57c00'
    }
  ]

  return (
    <Box>
      <Box sx={{ display: 'flex', alignItems: 'center', mb: 3 }}>
        <Dashboard sx={{ mr: 2, fontSize: 32 }} />
        <Typography variant="h4" component="h1">
          Admin Dashboard
        </Typography>
      </Box>
      
      <Paper sx={{ p: 3 }}>
        <Typography variant="h6" gutterBottom>
          Quick Actions
        </Typography>
        
        <Box sx={{ 
          display: 'grid', 
          gridTemplateColumns: { xs: '1fr', sm: 'repeat(2, 1fr)', md: 'repeat(3, 1fr)' },
          gap: 3 
        }}>
          {adminItems.map((item) => (
            <Card key={item.title} sx={{ height: '100%' }}>
              <CardActionArea 
                component={Link} 
                to={item.path}
                sx={{ height: '100%', p: 2 }}
              >
                <CardContent sx={{ textAlign: 'center' }}>
                  <Box sx={{ color: item.color, mb: 2 }}>
                    {item.icon}
                  </Box>
                  <Typography variant="h6" component="h2" gutterBottom>
                    {item.title}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    {item.description}
                  </Typography>
                </CardContent>
              </CardActionArea>
            </Card>
          ))}
        </Box>
      </Paper>
    </Box>
  )
}


