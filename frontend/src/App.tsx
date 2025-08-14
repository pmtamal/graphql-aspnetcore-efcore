import { AppBar, Toolbar, Typography, Button, Container, Box, IconButton, Drawer, List, ListItem, ListItemText, useTheme, useMediaQuery } from '@mui/material'
import { Menu, Logout } from '@mui/icons-material'
import { useState, useEffect } from 'react'
import { Link, Route, Routes, useNavigate } from 'react-router-dom'
import { CatalogPage } from './pages/CatalogPage'
import { BookDetailsPage } from './pages/BookDetailsPage'
import { LoginPage } from './pages/LoginPage'
import { RegisterPage } from './pages/RegisterPage'
import { CartPage } from './pages/CartPage'
import { CheckoutPage } from './pages/CheckoutPage'
import { AdminDashboard } from './pages/admin/AdminDashboard'
import { AdminBooksPage } from './pages/admin/AdminBooksPage'
import { AdminAuthorsPage } from './pages/admin/AdminAuthorsPage'
import { AdminCategoriesPage } from './pages/admin/AdminCategoriesPage'

interface UserProfile {
  id: number
  firstName: string
  lastName: string
  phone?: string
  address?: string
  city?: string
  state?: string
  postalCode?: string
  country?: string
  dateOfBirth?: string
  gender?: string
  bio?: string
  profilePicture?: string
  website?: string
  linkedIn?: string
  twitter?: string
  createdAt: string
  updatedAt?: string
}

interface User {
  id: number
  username: string
  email: string
  isAdmin: boolean
  isActive: boolean
  createdAt: string
  lastLoginAt?: string
  profile?: UserProfile
}

function Navbar() {
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))
  const [mobileOpen, setMobileOpen] = useState(false)
  const [user, setUser] = useState<User | null>(null)
  const navigate = useNavigate()

  useEffect(() => {
    const userStr = localStorage.getItem('user')
    if (userStr) {
      try {
        setUser(JSON.parse(userStr))
      } catch (error) {
        console.error('Error parsing user data:', error)
        localStorage.removeItem('user')
      }
    }
  }, [])

  const handleLogout = () => {
    localStorage.removeItem('user')
    setUser(null)
    navigate('/login')
  }

  const navItems = [
    { text: 'Catalog', path: '/catalog' },
    { text: 'Cart', path: '/cart' },
    ...(user?.isAdmin ? [
      { text: 'Admin', path: '/admin' },
      { text: 'Manage Books', path: '/admin/books' },
      { text: 'Manage Authors', path: '/admin/authors' },
      { text: 'Manage Categories', path: '/admin/categories' }
    ] : []),
    ...(user ? [] : [
      { text: 'Login', path: '/login' },
      { text: 'Register', path: '/register' }
    ])
  ]

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen)
  }

  const drawer = (
    <Box onClick={handleDrawerToggle} sx={{ textAlign: 'center' }}>
      <Typography variant="h6" sx={{ my: 2 }}>
        BookStore
      </Typography>
      {user && (
        <Typography variant="body2" sx={{ mb: 2, px: 2 }}>
          Welcome, {user.profile?.firstName || user.username}
          {user.isAdmin && ' (Admin)'}
        </Typography>
      )}
      <List>
        {navItems.map((item) => (
          <ListItem key={item.text} component={Link} to={item.path}>
            <ListItemText primary={item.text} />
          </ListItem>
        ))}
        {user && (
          <ListItem onClick={handleLogout}>
            <ListItemText primary="Logout" />
          </ListItem>
        )}
      </List>
    </Box>
  )

  return (
    <>
      <AppBar position="static">
        <Toolbar sx={{ px: { xs: 1, sm: 2 } }}>
          <Typography 
            variant="h6" 
            component={Link} 
            to="/" 
            sx={{ 
              flexGrow: 1, 
              textDecoration: 'none', 
              color: 'inherit',
              fontSize: { xs: '1.1rem', sm: '1.25rem' }
            }}
          >
            BookStore
          </Typography>
          
          {user && (
            <Typography 
              variant="body2" 
              sx={{ 
                mr: 2,
                display: { xs: 'none', sm: 'block' }
              }}
            >
              Welcome, {user.profile?.firstName || user.username}
              {user.isAdmin && ' (Admin)'}
            </Typography>
          )}
          
          {isMobile ? (
            <IconButton
              color="inherit"
              aria-label="open drawer"
              edge="start"
              onClick={handleDrawerToggle}
            >
              <Menu />
            </IconButton>
          ) : (
            <Box sx={{ display: 'flex', gap: 1, alignItems: 'center' }}>
              {navItems.map((item) => (
                <Button 
                  key={item.text}
                  color="inherit" 
                  component={Link} 
                  to={item.path}
                  sx={{ 
                    fontSize: { xs: '0.75rem', sm: '0.875rem' },
                    px: { xs: 1, sm: 2 }
                  }}
                >
                  {item.text}
                </Button>
              ))}
              {user && (
                <Button
                  color="inherit"
                  onClick={handleLogout}
                  startIcon={<Logout />}
                  sx={{ 
                    fontSize: { xs: '0.75rem', sm: '0.875rem' },
                    px: { xs: 1, sm: 2 }
                  }}
                >
                  Logout
                </Button>
              )}
            </Box>
          )}
        </Toolbar>
      </AppBar>
      
      <Drawer
        variant="temporary"
        open={mobileOpen}
        onClose={handleDrawerToggle}
        ModalProps={{
          keepMounted: true,
        }}
        sx={{
          display: { xs: 'block', md: 'none' },
          '& .MuiDrawer-paper': { boxSizing: 'border-box', width: 240 },
        }}
      >
        {drawer}
      </Drawer>
    </>
  )
}

export default function App() {

  return (
    <Box sx={{ 
      width: '100%', 
      minHeight: '100vh', 
      backgroundColor: '#f5f5f5',
      display: 'flex',
      flexDirection: 'column'
    }}>
      <Navbar />
      <Container 
        maxWidth="lg" 
        sx={{ 
          py: { xs: 2, sm: 4 },
          px: { xs: 1, sm: 2 },
          flex: 1,
          width: '100%'
        }}
      >
        <Routes>
          <Route path="/" element={<CatalogPage />} />
          <Route path="/catalog" element={<CatalogPage />} />
          <Route path="/book/:id" element={<BookDetailsPage />} />
          <Route path="/cart" element={<CartPage />} />
          <Route path="/checkout" element={<CheckoutPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/admin" element={<AdminDashboard />} />
          <Route path="/admin/books" element={<AdminBooksPage />} />
          <Route path="/admin/authors" element={<AdminAuthorsPage />} />
          <Route path="/admin/categories" element={<AdminCategoriesPage />} />
        </Routes>
      </Container>
    </Box>
  )
}

