import pika

# Define the callback function that will be executed when a message is received
def callback(ch, method, properties, body):
    # Decode the message body directly as a string
    message = body.decode()
    print(f"Received message: {message}")

# Define the connection parameters for RabbitMQ
connection_params = pika.ConnectionParameters('localhost')  # Update with RabbitMQ server details if not localhost

# Create a new connection to RabbitMQ
connection = pika.BlockingConnection(connection_params)

# Create a channel
channel = connection.channel()

# Declare the queue with durable=True to match the existing queue's properties
channel.queue_declare(queue='hello-message-queue', durable=True)

# Set up the consumer with the callback function
channel.basic_consume(queue='hello-message-queue', on_message_callback=callback, auto_ack=True)

print('Waiting for messages. To exit press CTRL+C')

# Start consuming messages
channel.start_consuming()
