import { 
  Box, 
  Typography, 
  Grid, 
  Card, 
  CardContent, 
  CardMedia, 
  CardActions, 
  Button, 
  TextField, 
  InputAdornment,
  Chip,
  Rating,
  Badge,
  IconButton,
  Drawer,
  List,
  ListItem,
  ListItemText,
  ListItemSecondaryAction,
  Divider,
  useTheme,
  useMediaQuery,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Slider,
  Accordion,
  AccordionSummary,
  AccordionDetails
} from '@mui/material'
import { 
  Search, 
  ShoppingCart, 
  AddShoppingCart, 
  RemoveShoppingCart,
  FilterList,
  ExpandMore,
  Star,
  Person,
  Book,
  Category,
  AttachMoney
} from '@mui/icons-material'
import { useState, useEffect } from 'react'
import { gql, useQuery } from '@apollo/client'

const GET_BOOKS = gql`
  query GetBooks {
    books {
      id
      title
      isbn
      description
      publicationYear
      publisher
      pages
      language
      price
      stockQuantity
      isAvailable
      coverImageUrl
      author {
        id
        firstName
        lastName
        fullName
      }
      category {
        id
        name
      }
      reviews {
        id
        rating
        comment
      }
    }
  }
`

const GET_CATEGORIES = gql`
  query GetCategories {
    categories {
      id
      name
      description
    }
  }
`

const GET_AUTHORS = gql`
  query GetAuthors {
    authors {
      id
      firstName
      lastName
      fullName
      nationality
    }
  }
`

interface Book {
  id: number
  title: string
  isbn: string
  description?: string
  publicationYear: number
  publisher?: string
  pages: number
  language?: string
  price: number
  stockQuantity: number
  isAvailable: boolean
  coverImageUrl?: string
  author: {
    id: number
    firstName: string
    lastName: string
    fullName: string
  }
  category: {
    id: number
    name: string
  }
  reviews: Array<{
    id: number
    rating: number
    comment?: string
  }>
}

interface CartItem {
  book: Book
  quantity: number
}

export function CustomerDashboard() {
  const theme = useTheme()
  const isMobile = useMediaQuery(theme.breakpoints.down('md'))
  
  // State
  const [searchTerm, setSearchTerm] = useState('')
  const [selectedCategory, setSelectedCategory] = useState<number | ''>('')
  const [selectedAuthor, setSelectedAuthor] = useState<number | ''>('')
  const [priceRange, setPriceRange] = useState<[number, number]>([0, 100])
  const [cartOpen, setCartOpen] = useState(false)
  const [cart, setCart] = useState<CartItem[]>([])
  const [filterOpen, setFilterOpen] = useState(false)

  // GraphQL Queries
  const { data: booksData, loading: booksLoading, error: booksError } = useQuery(GET_BOOKS)
  const { data: categoriesData } = useQuery(GET_CATEGORIES)
  const { data: authorsData } = useQuery(GET_AUTHORS)

  // Filter books based on search and filters
  const filteredBooks = booksData?.books?.filter((book: Book) => {
    const matchesSearch = book.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         book.author.fullName.toLowerCase().includes(searchTerm.toLowerCase()) ||
                         book.description?.toLowerCase().includes(searchTerm.toLowerCase())
    
    const matchesCategory = selectedCategory === '' || book.category.id === selectedCategory
    const matchesAuthor = selectedAuthor === '' || book.author.id === selectedAuthor
    const matchesPrice = book.price >= priceRange[0] && book.price <= priceRange[1]
    
    return matchesSearch && matchesCategory && matchesAuthor && matchesPrice
  }) || []

  // Cart functions
  const addToCart = (book: Book) => {
    setCart(prevCart => {
      const existingItem = prevCart.find(item => item.book.id === book.id)
      if (existingItem) {
        return prevCart.map(item =>
          item.book.id === book.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        )
      }
      return [...prevCart, { book, quantity: 1 }]
    })
  }

  const removeFromCart = (bookId: number) => {
    setCart(prevCart => prevCart.filter(item => item.book.id !== bookId))
  }

  const updateCartQuantity = (bookId: number, quantity: number) => {
    if (quantity <= 0) {
      removeFromCart(bookId)
      return
    }
    setCart(prevCart =>
      prevCart.map(item =>
        item.book.id === bookId
          ? { ...item, quantity }
          : item
      )
    )
  }

  const getCartTotal = () => {
    return cart.reduce((total, item) => total + (item.book.price * item.quantity), 0)
  }

  const getCartItemCount = () => {
    return cart.reduce((total, item) => total + item.quantity, 0)
  }

  const getAverageRating = (reviews: any[]) => {
    if (!reviews || reviews.length === 0) return 0
    return reviews.reduce((sum, review) => sum + review.rating, 0) / reviews.length
  }

  if (booksLoading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '60vh' }}>
        <Typography>Loading books...</Typography>
      </Box>
    )
  }

  if (booksError) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '60vh' }}>
        <Typography color="error">Error loading books: {booksError.message}</Typography>
      </Box>
    )
  }

  return (
    <Box sx={{ p: { xs: 2, sm: 3 } }}>
      {/* Header */}
      <Box sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between', mb: 3 }}>
        <Typography variant="h4" component="h1">
          Book Catalog
        </Typography>
        <Box sx={{ display: 'flex', gap: 1 }}>
          <IconButton
            color="primary"
            onClick={() => setFilterOpen(!filterOpen)}
            sx={{ display: { xs: 'block', md: 'none' } }}
          >
            <FilterList />
          </IconButton>
          <Badge badgeContent={getCartItemCount()} color="primary">
            <IconButton
              color="primary"
              onClick={() => setCartOpen(true)}
            >
              <ShoppingCart />
            </IconButton>
          </Badge>
        </Box>
      </Box>

      <Grid container spacing={3}>
        {/* Filters Sidebar */}
        <Grid item xs={12} md={3}>
          <Box sx={{ display: { xs: filterOpen ? 'block' : 'none', md: 'block' } }}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Filters
                </Typography>
                
                {/* Search */}
                <TextField
                  fullWidth
                  label="Search books..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  InputProps={{
                    startAdornment: (
                      <InputAdornment position="start">
                        <Search />
                      </InputAdornment>
                    ),
                  }}
                  sx={{ mb: 2 }}
                />

                {/* Category Filter */}
                <FormControl fullWidth sx={{ mb: 2 }}>
                  <InputLabel>Category</InputLabel>
                  <Select
                    value={selectedCategory}
                    label="Category"
                    onChange={(e) => setSelectedCategory(e.target.value as number | '')}
                  >
                    <MenuItem value="">All Categories</MenuItem>
                    {categoriesData?.categories?.map((category: any) => (
                      <MenuItem key={category.id} value={category.id}>
                        {category.name}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>

                {/* Author Filter */}
                <FormControl fullWidth sx={{ mb: 2 }}>
                  <InputLabel>Author</InputLabel>
                  <Select
                    value={selectedAuthor}
                    label="Author"
                    onChange={(e) => setSelectedAuthor(e.target.value as number | '')}
                  >
                    <MenuItem value="">All Authors</MenuItem>
                    {authorsData?.authors?.map((author: any) => (
                      <MenuItem key={author.id} value={author.id}>
                        {author.fullName}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>

                {/* Price Range */}
                <Typography gutterBottom>Price Range</Typography>
                <Slider
                  value={priceRange}
                  onChange={(_, newValue) => setPriceRange(newValue as [number, number])}
                  valueLabelDisplay="auto"
                  min={0}
                  max={100}
                  sx={{ mb: 2 }}
                />
                <Typography variant="body2" color="text.secondary">
                  ${priceRange[0]} - ${priceRange[1]}
                </Typography>
              </CardContent>
            </Card>
          </Box>
        </Grid>

        {/* Books Grid */}
        <Grid item xs={12} md={9}>
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
            <Typography variant="h6">
              {filteredBooks.length} books found
            </Typography>
          </Box>

          <Grid container spacing={2}>
            {filteredBooks.map((book: Book) => (
              <Grid item xs={12} sm={6} lg={4} key={book.id}>
                <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
                  <CardMedia
                    component="img"
                    height="200"
                    image={book.coverImageUrl || 'https://via.placeholder.com/300x200?text=No+Image'}
                    alt={book.title}
                    sx={{ objectFit: 'cover' }}
                  />
                  <CardContent sx={{ flexGrow: 1 }}>
                    <Typography variant="h6" component="h2" gutterBottom>
                      {book.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary" gutterBottom>
                      by {book.author.fullName}
                    </Typography>
                    <Typography variant="body2" color="text.secondary" gutterBottom>
                      {book.category.name} • {book.publicationYear}
                    </Typography>
                    
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 1 }}>
                      <Rating 
                        value={getAverageRating(book.reviews)} 
                        readOnly 
                        size="small"
                      />
                      <Typography variant="body2" color="text.secondary">
                        ({book.reviews.length} reviews)
                      </Typography>
                    </Box>

                    <Typography variant="h6" color="primary" gutterBottom>
                      ${book.price.toFixed(2)}
                    </Typography>

                    <Box sx={{ display: 'flex', gap: 1, mb: 1 }}>
                      <Chip 
                        label={book.isAvailable ? 'In Stock' : 'Out of Stock'} 
                        color={book.isAvailable ? 'success' : 'error'}
                        size="small"
                      />
                      {book.stockQuantity <= 5 && book.stockQuantity > 0 && (
                        <Chip 
                          label={`Only ${book.stockQuantity} left`} 
                          color="warning"
                          size="small"
                        />
                      )}
                    </Box>

                    {book.description && (
                      <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
                        {book.description.length > 100 
                          ? `${book.description.substring(0, 100)}...` 
                          : book.description}
                      </Typography>
                    )}
                  </CardContent>
                  <CardActions>
                    <Button
                      size="small"
                      startIcon={<AddShoppingCart />}
                      onClick={() => addToCart(book)}
                      disabled={!book.isAvailable}
                      fullWidth
                    >
                      Add to Cart
                    </Button>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Grid>
      </Grid>

      {/* Shopping Cart Drawer */}
      <Drawer
        anchor="right"
        open={cartOpen}
        onClose={() => setCartOpen(false)}
        PaperProps={{
          sx: { width: { xs: '100%', sm: 400 } }
        }}
      >
        <Box sx={{ p: 3 }}>
          <Typography variant="h6" gutterBottom>
            Shopping Cart ({getCartItemCount()} items)
          </Typography>
          
          {cart.length === 0 ? (
            <Typography color="text.secondary">
              Your cart is empty
            </Typography>
          ) : (
            <>
              <List>
                {cart.map((item) => (
                  <ListItem key={item.book.id} divider>
                    <Box sx={{ flexGrow: 1 }}>
                      <Typography variant="subtitle1">
                        {item.book.title}
                      </Typography>
                      <Typography variant="body2" color="text.secondary">
                        by {item.book.author.fullName}
                      </Typography>
                      <Typography variant="body2" color="primary">
                        ${item.book.price.toFixed(2)} × {item.quantity}
                      </Typography>
                    </Box>
                    <ListItemSecondaryAction>
                      <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                        <IconButton
                          size="small"
                          onClick={() => updateCartQuantity(item.book.id, item.quantity - 1)}
                        >
                          <RemoveShoppingCart />
                        </IconButton>
                        <Typography>{item.quantity}</Typography>
                        <IconButton
                          size="small"
                          onClick={() => updateCartQuantity(item.book.id, item.quantity + 1)}
                          disabled={item.quantity >= item.book.stockQuantity}
                        >
                          <AddShoppingCart />
                        </IconButton>
                      </Box>
                    </ListItemSecondaryAction>
                  </ListItem>
                ))}
              </List>
              
              <Divider sx={{ my: 2 }} />
              
              <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 2 }}>
                <Typography variant="h6">Total:</Typography>
                <Typography variant="h6" color="primary">
                  ${getCartTotal().toFixed(2)}
                </Typography>
              </Box>
              
              <Button
                variant="contained"
                fullWidth
                size="large"
                onClick={() => {
                  // TODO: Implement checkout
                  alert('Checkout functionality coming soon!')
                }}
              >
                Proceed to Checkout
              </Button>
            </>
          )}
        </Box>
      </Drawer>
    </Box>
  )
}

