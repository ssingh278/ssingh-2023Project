import React, { useState } from 'react';
import { View, ActivityIndicator, StyleSheet, Alert } from 'react-native';
import { Card, Avatar, Button, TextInput, Colors, Text } from 'react-native-paper';
import axios from 'axios';
import Clipboard from '@react-native-community/clipboard';

const customColors = {
  blue: '#3498db', // Replace with your custom blue color
  green: '#27ae60', // Replace with your custom green color
  white: '#ffffff', // Replace with your custom white color
  black:'#000000',
  red:'#FF0000'
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
     width:200,
        marginLeft:75
  },
  price: {
    fontSize: 16,
    fontWeight: 'bold',
    color: customColors.blue, // Use the custom blue color
  },
  cardText: {
    fontSize: 16,
    fontWeight: 'bold',
    color: customColors.black, // Use the custom blue color
  },
  expiry: {
    fontSize: 16,
    fontWeight: 'bold',
    color: customColors.red, // Use the custom blue color
  },
  editPriceInput: {
    fontSize: 16,
    color: '#333333',
  },
  buttonContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
  },
  saveButton: {
    backgroundColor: customColors.green, // Use the custom green color
  },
  cancelButton: {
    backgroundColor: '#e74c3c',
  },
  loadingIndicator: {
    marginTop: 10,
  },
});

export const AdminCardUI = ({ content, data, onUpdate }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [editedPrice, setEditedPrice] = useState(content.Price.toFixed(2));
  const [isLoading, setIsLoading] = useState(false);

  const handleEditClick = () => {
    setIsEditing(true);
  };

  const handleSaveClick = async () => {
    try {
      setIsLoading(true);

      const updatedPrice = parseFloat(editedPrice);
      if (!isNaN(updatePrice) && !isFinite(updatePrice)) {
        Alert.alert("Invalid Price");
        return
      }
      await updatePrice(content.Id, updatedPrice);

      onUpdate(data + 1);
      setIsLoading(false);
      setIsEditing(false);
    } catch (error) {
      Alert.alert('Error updating price:', error);
      setIsLoading(false);
    }
  };

  const handleCancelClick = () => {
    setIsEditing(false);
    setEditedPrice(content.Price.toFixed(2));
  };

  const get_image_url = (image_name) => {
    return `https://smartvendingimages.blob.core.windows.net/images/${image_name}`;
  };

  const updatePrice = async (itemId, newPrice) => {
    try {
      const response = await axios.put(
        `https://smartvendingmachine.azurewebsites.net/api/Inventory/updateprice/${itemId}`,
        newPrice,
        {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
          },
        }
      );

      if (response.status === 200) {
        Alert.alert('Price updated successfully');
      } else if (response.status === 404) {
        Alert.alert('Inventory item not found');
      } else {
        Alert.alert('An error occurred');
      }
    } catch (error) {
      Alert.alert('Network error:', error);
    }
  };

  return (
    <Card style={styles.card}>
      <Card.Cover source={{ uri: get_image_url(content.ImageName) }} style={styles.image} />
      <Card.Title
        title={content.Name}
        titleStyle={styles.cardTitle}
        subtitleStyle={styles.cardSubtitle}
      />

      <Card.Content>
        <Text style={styles.black}>ID: {content.Id}</Text>
        <Text style={styles.black}>Quantity: {content.Quantity}</Text>
        <Text style={styles.expiry}>Expiry Date: {content.ExpiryDate.split("T")[0]}</Text>

        {isLoading ? (
          <ActivityIndicator size="small" color={customColors.blue} style={styles.loadingIndicator} />
        ) : isEditing ? (
          <TextInput
            label="Price"
            value={editedPrice}
            onChangeText={(text) => setEditedPrice(text)}
            keyboardType="numeric"
            style={styles.editPriceInput}
            onFocus={() => Clipboard.setString('')}
            onSelectionChange={() => Clipboard.setString('')}
            maxLength={5}
          />
        ) : (
          <Text style={styles.price}>Price: ${content.Price.toFixed(2)}</Text>
        )}
      </Card.Content>
      <Card.Actions style={styles.buttonContainer}>
        {isLoading ? null : isEditing ? (
          <>
            <Button
              mode="contained"
              onPress={handleSaveClick}
              style={styles.saveButton}
              color={customColors.white}
            >
              Save
            </Button>
            <Button
              mode="contained"
              onPress={handleCancelClick}
              style={styles.cancelButton}
              color={customColors.white}
            >
              Cancel
            </Button>
          </>
        ) : (
          <Button mode="contained" onPress={handleEditClick} color={customColors.blue}>
            Edit
          </Button>
        )}

      </Card.Actions>
    </Card>
  );
};
