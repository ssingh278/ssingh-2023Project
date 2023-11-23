import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

const TotalSalesCard = ({ sum }) => {


    return (
        <View style={styles.totalSalesCard}>
            <Text style={styles.totalSalesText}>Total Sales: ${sum}</Text>
        </View>
    );
};

const styles = StyleSheet.create({
    totalSalesCard: {
        position: 'absolute',
        bottom: 0,
        width: '100%',
        backgroundColor: 'white',
        padding: 16,
        borderTopWidth: 1,
        borderColor: '#ccc',
    },
    totalSalesText: {
        fontSize: 18,
        fontWeight: 'bold',
        textAlign: 'center',
    },
});

export default TotalSalesCard;
