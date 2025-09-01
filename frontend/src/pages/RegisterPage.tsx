import { 
  Box, 
  Button, 
  TextField, 
  Typography, 
  Paper, 
  CircularProgress, 
  Alert,
  InputAdornment,
  IconButton,
  useTheme,
  useMediaQuery,
  Stepper,
  Step,
  StepLabel
} from '@mui/material'

import { 
  Visibility, 
  VisibilityOff, 
  Person, 
  Email, 
  Lock,
  CheckCircle,
  Error
} from '@mui/icons-material'
import { useState, useEffect } from 'react'
import { gql, useMutation } from '@apollo/client'
import { useNavigate, Link } from 'react-router-dom'

const REGISTER = gql`
  mutation Register($username: String!, $email: String!, $password: String!, $firstName: String!, $lastName: String!) {
    register(username: $username, email: $email, password: $password, firstName: $firstName, lastName: $lastName) {
      id
      username
      email
      isAdmin
      isActive
      createdAt
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

interface FormData {
  username: string
  email: string
  password: string
  confirmPassword: string
  firstName: string
  lastName: string
}

interface FormErrors {
  username?: string
  email?: string
  password?: string
  confirmPassword?: string
  firstName?: string
  lastName?: string
}

const steps = ['Account Details', 'Personal Information', 'Review & Submit']

export function RegisterPage() {
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))
  const navigate = useNavigate()
  
  const [register, { loading, error }] = useMutation(REGISTER)
  const [activeStep, setActiveStep] = useState(0)
  const [showPassword, setShowPassword] = useState(false)
  const [showConfirmPassword, setShowConfirmPassword] = useState(false)
  
  const [formData, setFormData] = useState<FormData>({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: ''
  })

  const [errors, setErrors] = useState<FormErrors>({})

  // Validation functions
  const validateUsername = (username: string): string | undefined => {
    if (!username) return 'Username is required'
    if (username.length < 3) return 'Username must be at least 3 characters'
    if (username.length > 20) return 'Username must be less than 20 characters'
    if (!/^[a-zA-Z0-9_]+$/.test(username)) return 'Username can only contain letters, numbers, and underscores'
    return undefined
  }

  const validateEmail = (email: string): string | undefined => {
    if (!email) return 'Email is required'
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    if (!emailRegex.test(email)) return 'Please enter a valid email address'
    return undefined
  }

  const validatePassword = (password: string): string | undefined => {
    if (!password) return 'Password is required'
    if (password.length < 6) return 'Password must be at least 6 characters'
    if (!/(?=.*[a-z])/.test(password)) return 'Password must contain at least one lowercase letter'
    if (!/(?=.*[A-Z])/.test(password)) return 'Password must contain at least one uppercase letter'
    if (!/(?=.*\d)/.test(password)) return 'Password must contain at least one number'
    return undefined
  }

  const validateConfirmPassword = (confirmPassword: string, password: string): string | undefined => {
    if (!confirmPassword) return 'Please confirm your password'
    if (confirmPassword !== password) return 'Passwords do not match'
    return undefined
  }

  const validateName = (name: string, fieldName: string): string | undefined => {
    if (!name) return `${fieldName} is required`
    if (name.length < 2) return `${fieldName} must be at least 2 characters`
    if (name.length > 50) return `${fieldName} must be less than 50 characters`
    return undefined
  }

  const validateStep = (step: number): boolean => {
    const newErrors: FormErrors = {}

    if (step === 0) {
      // Account Details
      newErrors.username = validateUsername(formData.username)
      newErrors.email = validateEmail(formData.email)
      newErrors.password = validatePassword(formData.password)
      newErrors.confirmPassword = validateConfirmPassword(formData.confirmPassword, formData.password)
    } else if (step === 1) {
      // Personal Information
      newErrors.firstName = validateName(formData.firstName, 'First name')
      newErrors.lastName = validateName(formData.lastName, 'Last name')
    }

    setErrors(newErrors)
    return !Object.values(newErrors).some(error => error !== undefined)
  }

  const handleNext = () => {
    if (validateStep(activeStep)) {
      setActiveStep((prevStep) => prevStep + 1)
    }
  }

  const handleBack = () => {
    setActiveStep((prevStep) => prevStep - 1)
  }

  const handleChange = (field: keyof FormData) => (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value
    setFormData(prev => ({ ...prev, [field]: value }))
    
    // Clear error when user starts typing
    if (errors[field]) {
      setErrors(prev => ({ ...prev, [field]: undefined }))
    }
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    
    if (!validateStep(activeStep)) {
      return
    }

    try {
      const result = await register({
        variables: {
          username: formData.username,
          email: formData.email,
          password: formData.password,
          firstName: formData.firstName,
          lastName: formData.lastName
        }
      })

      if (result.data?.register) {
        // Store user info in localStorage
        localStorage.setItem('user', JSON.stringify(result.data.register))
        
        // Redirect to catalog
        navigate('/catalog')
      }
    } catch (err) {
      console.error('Registration error:', err)
    }
  }

  const getStepContent = (step: number) => {
    switch (step) {
      case 0:
        return (
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
            <TextField
              fullWidth
              label="Username"
              value={formData.username}
              onChange={handleChange('username')}
              error={!!errors.username}
              helperText={errors.username}
              required
              size={isMobile ? "small" : "medium"}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <Person />
                  </InputAdornment>
                ),
              }}
            />
            <TextField
              fullWidth
              label="Email"
              type="email"
              value={formData.email}
              onChange={handleChange('email')}
              error={!!errors.email}
              helperText={errors.email}
              required
              size={isMobile ? "small" : "medium"}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <Email />
                  </InputAdornment>
                ),
              }}
            />
            <TextField
              fullWidth
              label="Password"
              type={showPassword ? 'text' : 'password'}
              value={formData.password}
              onChange={handleChange('password')}
              error={!!errors.password}
              helperText={errors.password || 'Must contain at least 6 characters, one uppercase, one lowercase, and one number'}
              required
              size={isMobile ? "small" : "medium"}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <Lock />
                  </InputAdornment>
                ),
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      onClick={() => setShowPassword(!showPassword)}
                      edge="end"
                    >
                      {showPassword ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
            <TextField
              fullWidth
              label="Confirm Password"
              type={showConfirmPassword ? 'text' : 'password'}
              value={formData.confirmPassword}
              onChange={handleChange('confirmPassword')}
              error={!!errors.confirmPassword}
              helperText={errors.confirmPassword}
              required
              size={isMobile ? "small" : "medium"}
              InputProps={{
                startAdornment: (
                  <InputAdornment position="start">
                    <Lock />
                  </InputAdornment>
                ),
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton
                      onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                      edge="end"
                    >
                      {showConfirmPassword ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
          </Box>
        )
      case 1:
        return (
          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
            <Box sx={{ display: 'flex', gap: 2, flexDirection: { xs: 'column', sm: 'row' } }}>
              <TextField
                fullWidth
                label="First Name"
                value={formData.firstName}
                onChange={handleChange('firstName')}
                error={!!errors.firstName}
                helperText={errors.firstName}
                required
                size={isMobile ? "small" : "medium"}
              />
              <TextField
                fullWidth
                label="Last Name"
                value={formData.lastName}
                onChange={handleChange('lastName')}
                error={!!errors.lastName}
                helperText={errors.lastName}
                required
                size={isMobile ? "small" : "medium"}
              />
            </Box>
          </Box>
        )
      case 2:
        return (
          <Box>
            <Typography variant="h6" gutterBottom>
              Review Your Information
            </Typography>
            <Box sx={{ bgcolor: 'grey.50', p: 2, borderRadius: 1, mb: 2 }}>
              <Typography variant="body2" gutterBottom>
                <strong>Username:</strong> {formData.username}
              </Typography>
              <Typography variant="body2" gutterBottom>
                <strong>Email:</strong> {formData.email}
              </Typography>
              <Typography variant="body2" gutterBottom>
                <strong>Name:</strong> {formData.firstName} {formData.lastName}
              </Typography>
            </Box>
            <Alert severity="info" sx={{ mb: 2 }}>
              By creating an account, you agree to our Terms of Service and Privacy Policy.
            </Alert>
          </Box>
        )
      default:
        return 'Unknown step'
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
          maxWidth: 600,
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
          Create Account
        </Typography>

        {/* Stepper */}
        <Stepper activeStep={activeStep} sx={{ mb: 4 }}>
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>
        
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error.message.includes('already exists') 
              ? 'Username or email already exists. Please try a different one.'
              : 'Registration failed. Please try again.'
            }
          </Alert>
        )}
        
        <Box component="form" onSubmit={handleSubmit}>
          {getStepContent(activeStep)}
          
          <Box sx={{ display: 'flex', justifyContent: 'space-between', mt: 3 }}>
            <Button
              disabled={activeStep === 0}
              onClick={handleBack}
              size={isMobile ? "small" : "medium"}
            >
              Back
            </Button>
            
            <Box>
              {activeStep === steps.length - 1 ? (
                <Button
                  variant="contained"
                  type="submit"
                  disabled={loading}
                  size={isMobile ? "small" : "medium"}
                  startIcon={loading ? <CircularProgress size={20} /> : <CheckCircle />}
                >
                  {loading ? 'Creating Account...' : 'Create Account'}
                </Button>
              ) : (
                <Button
                  variant="contained"
                  onClick={handleNext}
                  size={isMobile ? "small" : "medium"}
                >
                  Next
                </Button>
              )}
            </Box>
          </Box>
        </Box>

        <Box sx={{ mt: 3, textAlign: 'center' }}>
          <Typography variant="body2" color="text.secondary">
            Already have an account?{' '}
            <Link to="/login" style={{ color: theme.palette.primary.main, textDecoration: 'none' }}>
              Sign in here
            </Link>
          </Typography>
        </Box>
      </Paper>
    </Box>
  )
}


