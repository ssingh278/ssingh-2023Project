import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, ActivityIndicator, SafeAreaView, FlatList } from 'react-native';
import { Picker } from '@react-native-picker/picker';
import { AdminCardUI } from '../Components/AdminCardUI';
import axios from 'axios';
import SalesCardUI from '../Components/SalesCardUI';
import TotalSalesCard from '../Components/TotalSaleCardUI';
import LogsUI from '../Components/LogsUI'

const AdminDashboard = () => {
  const [selectedOption, setSelectedOption] = useState('Inventory');
  const [InventoryData, setInventoryData] = useState([]);
  const [SalesData, setSalesData] = useState([]);
  const [refresh, setRefresh] = useState(0);

  useEffect(() => {
    GetInventory();
    GetSales();
  }, [refresh]);

  async function GetInventory() {
    const baseURL = 'https://smartvendingmachine.azurewebsites.net/api/Inventory';

    try {
      // Make an API request using Axios
      const response = await axios.get(baseURL);
      const apiData = response.data;
      // Update the state with the data from the API response
      setInventoryData(apiData);
    } catch (error) {
      console.error('Error making API request:', error);
    }
  }

  async function GetSales() {
    const baseURL = 'https://smartvendingmachine.azurewebsites.net/api/Sales/GetSales';

    try {
      // Make an API request using Axios
      const response = await axios.get(baseURL);
      const apiData = response.data;
      // Update the state with the data from the API response
      setSalesData(apiData);
      console.log(SalesData)

    } catch (error) {
      console.error('Error making API request:', error);
    }
  }

  const TotalSale = () => {
    let totalSales = 0;

    SalesData.forEach((sale) => {
      totalSales += sale.Price;

    });

    return totalSales
  }
  // Define JSX elements for different options
  const elements = {
    Inventory: (
      InventoryData.length > 0 ? (
        <FlatList
          data={InventoryData}
          keyExtractor={(item) => item.Id.toString()}
          renderItem={({ item }) => <AdminCardUI content={item} data={refresh} onUpdate={setRefresh} />}
        />
      ) : (
        <ActivityIndicator size="large" style={styles.loadingIndicator} />
      )
    ),
    Sales: (
      SalesData.length > 0 ? (
        <View style={styles.salesContainer}>
          <FlatList
            data={SalesData}
            keyExtractor={(item) => item.sId.toString()}
            renderItem={({ item }) => <SalesCardUI content={item} />}
            contentContainerStyle={styles.flatListContent}
          />
          <TotalSalesCard sum={TotalSale()} />
        </View>

      ) : (
        <ActivityIndicator size="large" style={styles.loadingIndicator} />
      )


    ),
    Logs: <LogsUI/>,
  };

  return (
    <View style={styles.container}>
      <Picker
        selectedValue={selectedOption}
        onValueChange={(itemValue) => setSelectedOption(itemValue)}
      >
        <Picker.Item label="Inventory" value="Inventory" />
        <Picker.Item label="Sales" value="Sales" />
        <Picker.Item label="Logs" value="Logs" />
      </Picker>

      {/* Display the selected element */}
      {elements[selectedOption]}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F2F2F2', // Set your desired background color
  },
  loadingIndicator: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  salesContainer: {
    flex: 1
  },
  flatListContent: {
    paddingBottom: 100, // Add padding to separate the FlatList and Total Sales Card
  },
});


export default AdminDashboard;
