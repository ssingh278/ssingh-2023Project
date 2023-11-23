import React from 'react';
import { View, StyleSheet } from 'react-native';
import { Card, Text } from 'react-native-paper';

const customColors = {
    blue: '#3498db',
    green: '#27ae60',
    white: '#ffffff',
    black: '#000000',
    red: '#FF0000',
};

const styles = StyleSheet.create({
    card: {
        margin: 10,
        backgroundColor: '#FFFFFF',
    },
    cardTitle: {
        fontSize: 18,
        color: '#333333',
    },
    cardSubtitle: {
        color: '#555555',
    },
    image: {
        width: 200,
        marginLeft: 75,
    },
    price: {
        fontSize: 16,
        fontWeight: 'bold',
        color: customColors.blue,
    },
    expiry: {
        fontSize: 16,
        fontWeight: 'bold',
        color: customColors.green,
    },
    cardText: {
        fontSize: 16,
        fontWeight: 'bold',
        color: customColors.black,
    },
});

export default SalesCardUI = ({ content }) => {
    const get_image_url = (image_name) => {
        return `https://smartvendingimages.blob.core.windows.net/images/${image_name}`;
    };

    const dateObject = new Date(content.SaleTime);
    const formattedDate = dateObject.toLocaleString('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        hour12: true,
    });


    return (

        <Card style={styles.card}>
            <Card.Cover source={{ uri: get_image_url(content.ImageName) }} style={styles.image} />
            <Card.Title
                title={content.Name}
                titleStyle={styles.cardTitle}
                subtitleStyle={styles.cardSubtitle}
            />

            <Card.Content>
                <Text style={styles.expiry}>Sold on: {formattedDate}</Text>
                <Text style={styles.price}>Price: ${content.Price}</Text>
            </Card.Content>
        </Card>
    );
};

