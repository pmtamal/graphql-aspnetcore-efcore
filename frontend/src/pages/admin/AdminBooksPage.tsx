import { gql, useMutation, useQuery } from '@apollo/client'
import { 
  Box, 
  Button, 
  TextField, 
  Typography, 
  Paper, 
  CircularProgress, 
  Alert,
  List,
  ListItem,
  ListItemText,
  Divider,
  Chip,
  useTheme,
  useMediaQuery
} from '@mui/material'
import { useState } from 'react'
import { Link } from 'react-router-dom'
import { ArrowBack } from '@mui/icons-material'

const GET_BOOKS = gql`
  query GetBooks {
    books { id title author { fullName } category { name } }
  }
`

const ADD_BOOK = gql`
  mutation AddBook($title: String!, $authorId: Int!, $categoryId: Int!, $isbn: String!, $description: String!, $publicationYear: Int!, $publisher: String!, $pages: Int!, $language: String!, $price: Decimal!, $stockQuantity: Int!) {
    addBook(title: $title, authorId: $authorId, categoryId: $categoryId, isbn: $isbn, description: $description, publicationYear: $publicationYear, publisher: $publisher, pages: $pages, language: $language, price: $price, stockQuantity: $stockQuantity) {
      id
      title
    }
  }
`

export function AdminBooksPage() {
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))
  
  const { data, loading, error, refetch } = useQuery(GET_BOOKS)
  const [addBook, { loading: adding }] = useMutation(ADD_BOOK)

  const [formData, setFormData] = useState({
    title: '',
    authorId: '',
    categoryId: '',
    isbn: '',
    description: '',
    publicationYear: '',
    publisher: '',
    pages: '',
    language: '',
    price: '',
    stockQuantity: ''
  })

  const handleChange = (field: string) => (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData(prev => ({ ...prev, [field]: e.target.value }))
  }

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    const hasEmptyFields = Object.values(formData).some(value => !value)
    if (hasEmptyFields) return
    
    try {
      await addBook({ 
        variables: { 
          title: formData.title,
          authorId: Number(formData.authorId),
          categoryId: Number(formData.categoryId),
          isbn: formData.isbn,
          description: formData.description,
          publicationYear: Number(formData.publicationYear),
          publisher: formData.publisher,
          pages: Number(formData.pages),
          language: formData.language,
          price: Number(formData.price),
          stockQuantity: Number(formData.stockQuantity)
        } 
      })
      await refetch()
      setFormData({
        title: '',
        authorId: '',
        categoryId: '',
        isbn: '',
        description: '',
        publicationYear: '',
        publisher: '',
        pages: '',
        language: '',
        price: '',
        stockQuantity: ''
      })
    } catch (err) {
      console.error('Error adding book:', err)
    }
  }

  return (
    <Box sx={{ maxWidth: '100%' }}>
      <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 2 }}>
        <Typography 
          variant={isMobile ? "h5" : "h4"} 
          component="h1" 
          sx={{ fontSize: { xs: '1.5rem', sm: '2rem' } }}
        >
          Manage Books
        </Typography>
        <Button
          component={Link}
          to="/admin"
          variant="outlined"
          startIcon={<ArrowBack />}
          size={isMobile ? "small" : "medium"}
        >
          Back to Dashboard
        </Button>
      </Box>
      
      <Box sx={{ 
        display: 'grid', 
        gridTemplateColumns: { 
          xs: '1fr', 
          md: '1fr 1fr' 
        }, 
        gap: { xs: 3, md: 4 },
        maxWidth: '100%'
      }}>
        <Paper sx={{ p: { xs: 2, sm: 3 }, maxWidth: '100%' }}>
          <Typography 
            variant={isMobile ? "h6" : "h6"} 
            gutterBottom
            sx={{ fontSize: { xs: '1.1rem', sm: '1.25rem' } }}
          >
            Add New Book
          </Typography>
          
          <Box component="form" onSubmit={handleSubmit} sx={{ 
            display: 'flex', 
            flexDirection: 'column', 
            gap: { xs: 1.5, sm: 2 },
            maxWidth: '100%'
          }}>
            <TextField
              label="Title"
              value={formData.title}
              onChange={handleChange('title')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Author ID"
              type="number"
              value={formData.authorId}
              onChange={handleChange('authorId')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Category ID"
              type="number"
              value={formData.categoryId}
              onChange={handleChange('categoryId')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="ISBN"
              value={formData.isbn}
              onChange={handleChange('isbn')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Description"
              multiline
              rows={isMobile ? 2 : 3}
              value={formData.description}
              onChange={handleChange('description')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Publication Year"
              type="number"
              value={formData.publicationYear}
              onChange={handleChange('publicationYear')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Publisher"
              value={formData.publisher}
              onChange={handleChange('publisher')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Pages"
              type="number"
              value={formData.pages}
              onChange={handleChange('pages')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Language"
              value={formData.language}
              onChange={handleChange('language')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Price"
              type="number"
              value={formData.price}
              onChange={handleChange('price')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <TextField
              label="Stock Quantity"
              type="number"
              value={formData.stockQuantity}
              onChange={handleChange('stockQuantity')}
              required
              fullWidth
              size={isMobile ? "small" : "medium"}
            />
            <Button
              type="submit"
              variant="contained"
              disabled={adding}
              size={isMobile ? "medium" : "large"}
              sx={{ mt: 2 }}
            >
              {adding ? 'Creating...' : 'Create Book'}
            </Button>
          </Box>
        </Paper>
        
        <Paper sx={{ p: { xs: 2, sm: 3 }, maxWidth: '100%' }}>
          <Typography 
            variant={isMobile ? "h6" : "h6"} 
            gutterBottom
            sx={{ fontSize: { xs: '1.1rem', sm: '1.25rem' } }}
          >
            Existing Books
          </Typography>
          
          {loading && (
            <Box display="flex" justifyContent="center" p={2}>
              <CircularProgress />
            </Box>
          )}
          {error && <Alert severity="error">{error.message}</Alert>}
          
          {data?.books && (
            <List sx={{ p: 0, maxWidth: '100%' }}>
              {data.books.map((book: any, index: number) => (
                <Box key={book.id}>
                  <ListItem sx={{ px: { xs: 0, sm: 1 } }}>
                    <ListItemText
                      primary={
                        <Typography 
                          variant={isMobile ? "body2" : "body1"}
                          sx={{ fontWeight: 'bold' }}
                        >
                          {book.title}
                        </Typography>
                      }
                      secondary={
                        <Box sx={{ 
                          display: 'flex', 
                          gap: 1, 
                          mt: 1,
                          flexWrap: 'wrap'
                        }}>
                          <Chip 
                            label={book.author?.fullName || 'Unknown'} 
                            size="small" 
                            sx={{ fontSize: { xs: '0.7rem', sm: '0.75rem' } }}
                          />
                          <Chip 
                            label={book.category?.name || 'Uncategorized'} 
                            size="small" 
                            variant="outlined"
                            sx={{ fontSize: { xs: '0.7rem', sm: '0.75rem' } }}
                          />
                        </Box>
                      }
                    />
                  </ListItem>
                  {index < data.books.length - 1 && <Divider />}
                </Box>
              ))}
            </List>
          )}
        </Paper>
      </Box>
    </Box>
  )
}


