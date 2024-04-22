
# Start from a Node.js base image
FROM node

# Set the working directory in the Docker image
WORKDIR /app

# Copy package.json and package-lock.json (if available)
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of your application
COPY public/ /app/public
COPY src/ /app/src

# Expose the port your app runs on
EXPOSE 3000

# Start the application
CMD ["npm", "start"]