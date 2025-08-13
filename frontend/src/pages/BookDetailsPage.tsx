import { gql, useQuery } from '@apollo/client'
import { 
  Box, 
  Button, 
  Typography, 
  Rating, 
  Chip, 
  Paper, 
  Divider,
  CircularProgress,
  Alert,
  useTheme,
  useMediaQuery
} from '@mui/material'
import { useParams } from 'react-router-dom'

const GET_BOOK = gql`
  query GetBook($id: Int!) {
    bookById(id: $id) {
      id
      title
      description
      coverImageUrl
      formattedPrice
      author { fullName }
      category { name }
      averageRating
      reviewCount
    }
  }
`

export function BookDetailsPage() {
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))
  
  const params = useParams()
  const id = Number(params.id)
  const { data, loading, error } = useQuery(GET_BOOK, { variables: { id }, skip: !id })

  if (!id) return <Alert severity="error">Invalid book ID</Alert>
  if (loading) return (
    <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
      <CircularProgress />
    </Box>
  )
  if (error) return <Alert severity="error">{error.message}</Alert>

  const book = data?.bookById
  if (!book) return <Alert severity="warning">Book not found</Alert>

  return (
    <Paper sx={{ p: { xs: 2, sm: 3 }, maxWidth: '100%' }}>
      <Box sx={{ 
        display: 'grid', 
        gridTemplateColumns: { 
          xs: '1fr', 
          md: '320px 1fr' 
        }, 
        gap: { xs: 2, sm: 4 },
        alignItems: 'start',
        maxWidth: '100%'
      }}>
        <Box sx={{ 
          display: 'flex', 
          justifyContent: { xs: 'center', md: 'flex-start' },
          mb: { xs: 2, md: 0 }
        }}>
          {book.coverImageUrl && (
            <Box
              component="img"
              src={book.coverImageUrl}
              alt={book.title}
              sx={{
                width: '100%',
                maxWidth: { xs: '280px', sm: '320px' },
                height: 'auto',
                borderRadius: 2,
                boxShadow: 3
              }}
            />
          )}
        </Box>
        <Box sx={{ maxWidth: '100%' }}>
          <Typography 
            variant={isMobile ? "h4" : "h3"} 
            component="h1" 
            gutterBottom
            sx={{ 
              fontSize: { xs: '1.5rem', sm: '2rem', md: '3rem' },
              lineHeight: 1.2,
              wordBreak: 'break-word'
            }}
          >
            {book.title}
          </Typography>
          
          <Typography 
            variant={isMobile ? "body1" : "h6"} 
            color="text.secondary" 
            gutterBottom
            sx={{ fontSize: { xs: '1rem', sm: '1.25rem' } }}
          >
            by {book.author?.fullName || 'Unknown Author'}
          </Typography>
          
          <Box sx={{ 
            display: 'flex', 
            gap: 1, 
            mb: 2,
            flexWrap: 'wrap'
          }}>
            <Chip 
              label={book.category?.name || 'Uncategorized'} 
              color="primary" 
              size={isMobile ? "small" : "medium"}
            />
            <Chip 
              label={book.formattedPrice} 
              variant="outlined" 
              size={isMobile ? "small" : "medium"}
            />
          </Box>
          
          <Box sx={{ 
            display: 'flex', 
            alignItems: 'center', 
            gap: 1, 
            mb: 2,
            flexWrap: 'wrap'
          }}>
            <Rating 
              value={book.averageRating} 
              precision={0.5} 
              size={isMobile ? "small" : "medium"}
              readOnly 
            />
            <Typography 
              variant="body2" 
              color="text.secondary"
              sx={{ fontSize: { xs: '0.875rem', sm: '1rem' } }}
            >
              {book.averageRating.toFixed(1)} ({book.reviewCount} reviews)
            </Typography>
          </Box>
          
          <Divider sx={{ my: 2 }} />
          
          <Typography 
            variant="body1" 
            paragraph
            sx={{ 
              fontSize: { xs: '0.875rem', sm: '1rem' },
              lineHeight: 1.6,
              wordBreak: 'break-word'
            }}
          >
            {book.description || 'No description available.'}
          </Typography>
          
          <Box sx={{ 
            mt: 3,
            display: 'flex',
            flexDirection: { xs: 'column', sm: 'row' },
            gap: { xs: 1, sm: 2 }
          }}>
            <Button 
              variant="contained" 
              size={isMobile ? "medium" : "large"}
              fullWidth={isMobile}
            >
              Add to Cart
            </Button>
            <Button 
              variant="outlined" 
              size={isMobile ? "medium" : "large"}
              fullWidth={isMobile}
            >
              Write Review
            </Button>
          </Box>
        </Box>
      </Box>
    </Paper>
  )
}


