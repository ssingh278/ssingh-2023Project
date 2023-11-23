import {StyleSheet, Text, View,FlatList,ActivityIndicator } from 'react-native'
import React, { useState, useEffect } from 'react'
import axios from 'axios'
import { SafeAreaView } from 'react-native-safe-area-context'
import GuestCardUI from '../../Components/GuestCardUI';

export default function GuestData() {
    const [allData, setallData] = useState([]);
    useEffect(() => {
        fetchDataFromAPI();
    }, [])

    async function fetchDataFromAPI() {
        const baseURL = 'https://smartvendingmachine.azurewebsites.net/api/Inventory';

        try {
            // Make an API request using Axios
            const response = await axios.get(baseURL);
            const apiData = response.data;
            // Update the state with the data from the API response
            setallData(apiData);
        } catch (error) {
            console.error('Error making API request:', error);
        }
    }

    return (

        (allData.length != 0) ?
            <SafeAreaView style={{ backgroundColor: '#8e44ad' }}>
                <View style={{ alignItems: 'center' }}>
                    <Text style={{ fontSize: 50 }}>Inventory</Text>
                </View>
                <FlatList
                    data={allData}
                    keyExtractor={(item) => item.Id.toString()}
                    renderItem={({ item }) => (
                        // <FancyCards arr={item} />
                        <GuestCardUI content={item} />
                    )}
                />
               
            </SafeAreaView> :
            <View style={[styles.container, styles.horizontal]}>
                <ActivityIndicator size="large" />
            </View>

    )
}
const styles = StyleSheet.create({

    horizontal: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        padding: 10,

        container: {
            flex: 1,
            justifyContent: 'center',
            alignItems: 'center',
            backgroundColor: '#f0f0f0',
        }
    }
});
