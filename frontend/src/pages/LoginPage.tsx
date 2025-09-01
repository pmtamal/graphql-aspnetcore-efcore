import { gql, useLazyQuery } from '@apollo/client'
import { 
  Box, 
  Button, 
  TextField, 
  Typography, 
  Paper, 
  CircularProgress, 
  Alert,
  useTheme,
  useMediaQuery
} from '@mui/material'
import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'

const LOGIN = gql`
  query Login($username: String!, $password: String!) {
    login(username: $username, password: $password) {
      id
      username
      email
      isAdmin
      isActive
      createdAt
      lastLoginAt
      profile {
        id
        firstName
        lastName
        phone
        address
        city
        state
        postalCode
        country
        dateOfBirth
        gender
        bio
        profilePicture
        website
        linkedIn
        twitter
        createdAt
        updatedAt
      }
    }
  }
`

export function LoginPage() {
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))
  const navigate = useNavigate()
  
  const [login, { loading, error }] = useLazyQuery(LOGIN)
  const [formData, setFormData] = useState({
    username: '',
    password: ''
  })

  // Reset form data when component mounts (after logout)
  useEffect(() => {
    setFormData({
      username: '',
      password: ''
    })
  }, [])

  const handleChange = (field: string) => (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData(prev => ({ ...prev, [field]: e.target.value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    
    try {
      const result = await login({ 
        variables: { 
          username: formData.username,
          password: formData.password
        } 
      })
      
      if (result.data?.login) {
        const user = result.data.login
        // Store user info in localStorage
        localStorage.setItem('user', JSON.stringify(user))
        
        // Redirect based on user role
        if (user.isAdmin) {
          navigate('/admin')
        } else {
          navigate('/catalog')
        }
      }
    } catch (err) {
      console.error('Login error:', err)
    }
  }

  return (
    <Box 
      sx={{ 
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        p: { xs: 2, sm: 3 }
      }}
    >
      <Paper 
        sx={{ 
          p: { xs: 3, sm: 4 },
          maxWidth: 400,
          width: '100%'
        }}
      >
        <Typography 
          variant={isMobile ? "h5" : "h4"} 
          component="h1" 
          gutterBottom
          align="center"
          sx={{ mb: 3 }}
        >
          Login
        </Typography>
        
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            Invalid username or password
          </Alert>
        )}
        
        <Box component="form" onSubmit={handleSubmit} sx={{ 
          display: 'flex', 
          flexDirection: 'column', 
          gap: 2
        }}>
          <TextField
            label="Username"
            value={formData.username}
            onChange={handleChange('username')}
            required
            fullWidth
            size={isMobile ? "small" : "medium"}
            autoFocus
          />
          <TextField
            label="Password"
            type="password"
            value={formData.password}
            onChange={handleChange('password')}
            required
            fullWidth
            size={isMobile ? "small" : "medium"}
          />
          <Button
            type="submit"
            variant="contained"
            disabled={loading}
            size={isMobile ? "medium" : "large"}
            sx={{ mt: 2 }}
          >
            {loading ? <CircularProgress size={24} /> : 'Login'}
          </Button>
        </Box>
        
        <Box sx={{ mt: 3, textAlign: 'center' }}>
          <Typography variant="body2" color="text.secondary">
            Demo Admin Account:
          </Typography>
          <Typography variant="body2" color="text.secondary">
            Username: admin
          </Typography>
          <Typography variant="body2" color="text.secondary">
            Password: admin123
          </Typography>
        </Box>
      </Paper>
    </Box>
  )
}


