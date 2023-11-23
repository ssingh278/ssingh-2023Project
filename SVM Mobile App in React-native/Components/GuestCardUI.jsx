//***********************************************************************************
/**
 * This JSX page will create the layouit of the Card that needs to be displayed to the user 
 */

//***********************************************************************************


//***********************************************************************************
// All Imports go here
import { Card, Text } from 'react-native-paper';
import {StyleSheet,View} from 'react-native'


/**
 * This function will display the Card on Guest Page
 * @param {*} Content -> The prop that will accept the item with all the properties 
 * @returns JSX -> Card definition
 */
const GuestCardUI = ({ content }) => {
    const get_image_url = (image_name) => {
        return `https://smartvendingimages.blob.core.windows.net/images/${image_name}`
    }
    return (
        <View style={{margin:10,padding:10}}>
        <Card style={{borderRadius:20,borderColor:'#000000'}}>
            <Card.Content style={styles.contentContainer}>
                <Text variant='titleLarge'>Name : {content.Name}</Text>
                <Text variant='bodyMedium'>Price: ${content.Price.toFixed(2)}</Text>
                <Text variant='bodyMedium'>Quantity: {content.Quantity}</Text>
                <Text variant='bodyMedium'>Expiry Date: {content.ExpiryDate.split("T")[0]}</Text>
            </Card.Content>
            <Card.Cover source={{ uri: get_image_url(content.ImageName) }} style={styles.img}/>
        </Card>
        </View>
    );
}

/**
 * The styles required for the Card
 */
const styles = StyleSheet.create({
    contentContainer: {
        backgroundColor:'#9b59b6',
        borderTopLeftRadius:20,
        borderTopRightRadius:20
    },
    img:{
        width:200,
        marginLeft:75
    }
});

export default GuestCardUI;