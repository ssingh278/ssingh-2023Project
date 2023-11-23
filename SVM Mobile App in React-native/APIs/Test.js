import { StyleSheet, Text, View, Button, FlatList, Image } from 'react-native'
import React, { useState, useEffect } from 'react'
import axios from 'axios'

export default function Test() {

    const baseURL = 'https://smartvendingmachine.azurewebsites.net/api/Inventory';


    const [myArray, setMyArray] = useState([]);

    useEffect(() => {
        // Define an asynchronous function to make the API request
        async function fetchDataFromAPI() {
            try {
                // Make an API request using Axios
                const response = await axios.get(baseURL);
                const apiData = response.data;
                // Update the state with the data from the API response
                setMyArray(apiData);
                console.log(myArray)

            } catch (error) {
                console.error('Error making API request:', error);
            }
        }

        // Call the API when the component mounts
        fetchDataFromAPI();
    }, []);

    const [isClick, setIsClick] = useState(false);
    
    function ClickMe()
    {
        if (isClick === false)
            setIsClick(true);
        else
            setIsClick(false);
    }

    return (
        <View >
             <Image source={{uri:`https://smartvendingimages.blob.core.windows.net/images/Chips.jpg}`}} style={styles.image}/>

            <Button title='GET' onPress={ClickMe}/>
            {isClick && myArray.map((item, index) => {
                return (
                    <View key={item.Id} style={{ color: '#000' }}>
                        <Text style={{ color: '#000' }}>Name : {item.Name}</Text>
                        <Text style={{ color: '#000' }}>Price:{item.Price}</Text>
                        <Text style={{ color: '#000' }}>Quantity :{item.Quantity}</Text>
                        <Text style={{ color: '#000' }}>Date :{item.ExpiryDate}</Text>
                        <Image source={{uri:`https://smartvendingimages.blob.core.windows.net/images/${item.ImageName}}`}} style={styles.image}/>
                        <Text style={{ color: '#000' }}>---------------------------------------------------</Text>

                    </View>
                )
            })}
             <Image source={{uri:`https://smartvendingimages.blob.core.windows.net/images/Chips.jpg}`}} style={styles.image}/>

        </View>
    )
}

const styles = StyleSheet.create({
    image: {
        width: 200, // Set an appropriate width
        height: 200, // Set an appropriate height
        color:'black'
      },
})