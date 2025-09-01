import { AppBar, Toolbar, Typography, Button, Container, Box, IconButton, Drawer, List, ListItem, ListItemText, useTheme, useMediaQuery, Menu, MenuItem, Avatar, Divider } from '@mui/material'
import { Menu as MenuIcon, Logout, Person, AdminPanelSettings } from '@mui/icons-material'
import { useState, useEffect } from 'react'
import { Link, Route, Routes, useNavigate } from 'react-router-dom'
import { useApolloClient } from '@apollo/client'
import { logout } from './utils/auth'
import { CustomerDashboard } from './pages/CustomerDashboard'
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
  const [profileMenuAnchor, setProfileMenuAnchor] = useState<null | HTMLElement>(null)
  const [user, setUser] = useState<User | null>(null)
  const navigate = useNavigate()
  const client = useApolloClient()

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
    logout(client, navigate)
    setProfileMenuAnchor(null)
  }

  const handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setProfileMenuAnchor(event.currentTarget)
  }

  const handleProfileMenuClose = () => {
    setProfileMenuAnchor(null)
  }

  const navItems = [
    { text: 'Book Catalog', path: '/catalog' },
    ...(user?.isAdmin ? [
      { text: 'Admin Dashboard', path: '/admin' }
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
          <>
            <Divider sx={{ my: 1 }} />
            <ListItem>
              <ListItemText 
                primary={user.profile?.firstName || user.username}
                secondary={user.email}
              />
            </ListItem>
            {user.isAdmin && (
              <ListItem>
                <ListItemText 
                  primary="Administrator"
                  primaryTypographyProps={{ color: 'primary' }}
                />
              </ListItem>
            )}
            <ListItem onClick={handleLogout}>
              <ListItemText primary="Logout" />
            </ListItem>
          </>
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
          
          {isMobile ? (
            <IconButton
              color="inherit"
              aria-label="open drawer"
              edge="start"
              onClick={handleDrawerToggle}
            >
              <MenuIcon />
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
                <IconButton
                  color="inherit"
                  onClick={handleProfileMenuOpen}
                  sx={{ ml: 1 }}
                >
                  <Avatar 
                    sx={{ 
                      width: 32, 
                      height: 32, 
                      bgcolor: 'primary.main',
                      fontSize: '0.875rem'
                    }}
                  >
                    {user.profile?.firstName?.charAt(0) || user.username.charAt(0).toUpperCase()}
                  </Avatar>
                </IconButton>
              )}
            </Box>
          )}
        </Toolbar>
      </AppBar>

      {/* Profile Menu */}
      <Menu
        anchorEl={profileMenuAnchor}
        open={Boolean(profileMenuAnchor)}
        onClose={handleProfileMenuClose}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'right',
        }}
        transformOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        PaperProps={{
          sx: {
            mt: 1,
            minWidth: 200,
          }
        }}
      >
        {user && (
          <>
            <MenuItem disabled sx={{ opacity: 0.7 }}>
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                <Person fontSize="small" />
                <Box>
                  <Typography variant="body2" sx={{ fontWeight: 500 }}>
                    {user.profile?.firstName || user.username}
                  </Typography>
                  <Typography variant="caption" color="text.secondary">
                    {user.email}
                  </Typography>
                </Box>
              </Box>
            </MenuItem>
            {user.isAdmin && (
              <MenuItem disabled sx={{ opacity: 0.7 }}>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                  <AdminPanelSettings fontSize="small" color="primary" />
                  <Typography variant="body2" color="primary">
                    Administrator
                  </Typography>
                </Box>
              </MenuItem>
            )}
            <Divider />
            <MenuItem onClick={handleLogout}>
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                <Logout fontSize="small" />
                <Typography>Logout</Typography>
              </Box>
            </MenuItem>
          </>
        )}
      </Menu>
      
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
          <Route path="/" element={<CustomerDashboard />} />
          <Route path="/catalog" element={<CustomerDashboard />} />
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

