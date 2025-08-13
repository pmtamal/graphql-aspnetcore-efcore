import { gql, useQuery } from '@apollo/client'
import { 
  Card, 
  CardContent, 
  CardMedia, 
  Typography, 
  Rating, 
  Box, 
  Chip,
  CircularProgress,
  Alert,
  useTheme,
  useMediaQuery
} from '@mui/material'
import { Link } from 'react-router-dom'

const GET_BOOKS = gql`
  query GetBooks {
    books {
      id
      title
      formattedPrice
      author { 
        id
        fullName 
      }
      category { 
        id
        name 
      }
    }
  }
`

type Book = {
  id: number
  title: string
  coverImageUrl?: string | null
  formattedPrice: string
  author?: { id: number; fullName: string } | null
  category?: { id: number; name: string } | null
  averageRating?: number
  reviewCount?: number
}

export function CatalogPage() {
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'))
  
  const { data, loading, error } = useQuery<{ books: Book[] }>(GET_BOOKS)

  if (loading) return (
    <Box display="flex" justifyContent="center" alignItems="center" minHeight="400px">
      <CircularProgress />
    </Box>
  )
  
  if (error) return (
    <Alert severity="error" sx={{ mb: 2 }}>
      {error.message}
    </Alert>
  )

  const books = data?.books ?? []

  return (
    <Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Book Catalog
      </Typography>
      <Box sx={{ 
        display: 'grid', 
        gridTemplateColumns: {
          xs: '1fr',
          sm: 'repeat(2, 1fr)',
          md: 'repeat(3, 1fr)',
          lg: 'repeat(4, 1fr)',
          xl: 'repeat(5, 1fr)'
        },
        gap: { xs: 2, sm: 3 },
        maxWidth: '100%',
        mx: 'auto'
      }}>
        {books.map((book) => (
          <Card key={book.id} sx={{ 
            height: '100%', 
            display: 'flex', 
            flexDirection: 'column',
            minHeight: isMobile ? 'auto' : '400px',
            maxWidth: '100%'
          }}>
            {book.coverImageUrl && (
              <CardMedia
                component="img"
                height={isMobile ? "150" : "200"}
                image={book.coverImageUrl}
                alt={book.title}
                sx={{ objectFit: 'cover' }}
              />
            )}
            <CardContent sx={{ 
              flexGrow: 1, 
              display: 'flex', 
              flexDirection: 'column',
              p: { xs: 1.5, sm: 2 }
            }}>
              <Typography 
                gutterBottom 
                variant={isMobile ? "body1" : "h6"} 
                component="h2" 
                noWrap
                sx={{ fontWeight: 'bold' }}
              >
                {book.title}
              </Typography>
              <Typography 
                variant="body2" 
                color="text.secondary" 
                gutterBottom
                sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}
              >
                by {book.author?.fullName || 'Unknown Author'}
              </Typography>
              <Chip 
                label={book.category?.name || 'Uncategorized'} 
                size="small" 
                sx={{ 
                  alignSelf: 'flex-start', 
                  mb: 1,
                  fontSize: { xs: '0.7rem', sm: '0.75rem' }
                }}
              />
              <Box sx={{ mt: 'auto', pt: 1 }}>
                <Typography 
                  variant={isMobile ? "body1" : "h6"} 
                  color="primary" 
                  gutterBottom
                  sx={{ fontWeight: 'bold' }}
                >
                  {book.formattedPrice}
                </Typography>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                  <Rating 
                    value={book.averageRating} 
                    precision={0.5} 
                    size={isMobile ? "small" : "medium"} 
                    readOnly 
                  />
                  <Typography 
                    variant="body2" 
                    color="text.secondary"
                    sx={{ fontSize: { xs: '0.7rem', sm: '0.875rem' } }}
                  >
                    ({book.reviewCount})
                  </Typography>
                </Box>
                <Typography
                  component={Link}
                  to={`/book/${book.id}`}
                  variant="button"
                  color="primary"
                  sx={{ 
                    textDecoration: 'none',
                    fontSize: { xs: '0.75rem', sm: '0.875rem' },
                    display: 'block',
                    mt: 1
                  }}
                >
                  View Details
                </Typography>
              </Box>
            </CardContent>
          </Card>
        ))}
      </Box>
    </Box>
  )
}


