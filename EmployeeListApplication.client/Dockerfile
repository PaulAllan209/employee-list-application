# Use Node.js as base image
FROM node:latest AS build

# Set working directory
WORKDIR /app

# Copy package files
COPY package*.json ./

# Install dependencies
RUN npm ci

# Install Angular CLI globally
RUN npm install -g @angular/cli

# Copy source code
COPY . .

# # Remove any existing node modules specific to windows to avoid platform conflicts
# RUN rm -rf node_modules

# # Reinstall dependencies for the Linux platform
# RUN npm ci

# Build the Angular app
RUN npm run build -- --configuration=docker

# Debug: Let's see what's actually built
RUN echo "=== Contents of /app/dist ==="
RUN ls -la /app/dist/
RUN echo "=== Checking if there's a subfolder ==="
RUN find /app/dist -type d
RUN echo "=== All files in dist ==="
RUN find /app/dist -type f

# Use Nginx to serve the built app
FROM nginx:latest

# Copy the build output to replace the default nginx contents
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
RUN rm -rf /usr/share/nginx/html/* 

# Copy built app from build stage
COPY --from=build /app/dist/employee-list-application.client/browser /usr/share/nginx/html

# Debug: Let's see what actually got copied to nginx
RUN echo "=== Contents of nginx html folder ==="
RUN ls -la /usr/share/nginx/html/

# Expose port 80
EXPOSE 80

# # Start Nginx
CMD ["nginx", "-g", "daemon off;"]