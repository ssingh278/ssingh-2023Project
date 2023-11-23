import React, { useEffect, useState } from 'react';
import { View, Text, FlatList, StyleSheet,ActivityIndicator } from 'react-native';
import axios from 'axios';

const LogPage = () => {
  const [logs, setLogs] = useState([]);

  async function GetLogs() {
    const baseURL = 'https://smartvendingmachine.azurewebsites.net/api/Logs/GetLogs';

    try {
      // Make an API request using Axios
      const response = await axios.get(baseURL);
      const apiData = response.data;
      // Update the state with the data from the API response
      setLogs(apiData);
      console.log(apiData);

    } catch (error) {
      console.error('Error making API request:', error);
    }
  }
  useEffect(() => {
    // Fetch logs from your API or any data source
    // For now, let's simulate some logs
    GetLogs();
  }, []);

  const renderItem = ({ item }) => {
    const dateObject = new Date(item.CreatedDate);
    const formattedDate = dateObject.toLocaleString('en-US', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        hour12: true,
    });

    return(<View style={styles.logItem}>
        <Text>{`Name: ${item.Name}`}</Text>
        <Text>{`Description: ${item.Description}`}</Text>
        <Text>{` Date and Time: ${formattedDate}`}</Text>
      </View>)
    
    };

  return (
    logs.length > 0 ? ( <View style={styles.container}>
        <Text style={styles.header}>Logs</Text>
        <FlatList
          data={logs}
          keyExtractor={(item) => item.Id.toString()}
          renderItem={renderItem}
        />
      </View>):
       (
        <ActivityIndicator size="large" style={styles.loadingIndicator} />
      )
   
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
  },
  header: {
    fontSize: 20,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  logItem: {
    borderBottomWidth: 1,
    borderBottomColor: '#ddd',
    paddingVertical: 8,
    marginBottom: 8,
  },
  loadingIndicator: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  }
});

export default LogPage;
