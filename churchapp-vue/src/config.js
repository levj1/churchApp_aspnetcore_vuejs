export default{
    ENV: process.env.NODE_ENV,
    BASE_URL: process.env.NODE_ENV === 'production' ? 'http://jameslevj1-001-site2.gtempurl.com' 
    : 'https://localhost:44325',
}