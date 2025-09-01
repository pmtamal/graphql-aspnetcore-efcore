import { ApolloClient } from '@apollo/client'

export const logout = (client: ApolloClient<any>, navigate: (path: string) => void) => {
  // Clear localStorage
  localStorage.removeItem('user')
  
  // Clear Apollo Client cache
  client.clearStore()
  
  // Reset Apollo Client cache
  client.resetStore()
  
  // Navigate to login page
  navigate('/login')
}
