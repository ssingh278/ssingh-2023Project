//***********************************************************************************/
//Program: Mobile App for Smart Vending Machine
//Description:  This file create the function that will provide a user an option with either
//             they are guest or the admin
//Date: 10-Oct -2023
//Author: Sharry Singh
//***********************************************************************************/

//***********************************************************************************/
// All import goes here
import React from 'react';
import { View, Text, TouchableOpacity, ImageBackground, StyleSheet } from 'react-native';
import styled from 'styled-components/native';

//***********************************************************************************/


//***********************************************************************************/
//All the styling goes here
const BackgroundImage = styled(ImageBackground)`
  flex: 1;
  justify-content: center;
  align-items: center;
`;

const Button = styled.TouchableOpacity`
  background-color: #a55eea;
  padding: 15px 60px;
  border-radius: 5px;
  margin: 10px;
`;

const ButtonText = styled.Text`
  color: #fff;
  font-size: 20px;
`;


const styles = StyleSheet.create({
  mainView: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  headView: {
    flex: 0,
    justifyContent: 'center',
    alignItems: 'center',
    flexDirection: 'row'
  }
  ,
  headingText: {
    fontSize: 60,         // Adjust the font size as needed
    fontWeight: 'bold',  // Make the text bold
    color: '#a55eea',    // Text color on the background image
    textShadowColor: 'rgba(255, 255, 255, 1)', // Black shadow color with full opacity
    textShadowOffset: { width: 2, height: 2 }, // Shadow offset (adjust as needed)
    textShadowRadius: 4, // Shadow radius (adjust as needed)
    marginRight: 'auto',
    paddingTop: 20
  },
  icon: {
    marginLeft: 10, // Add some space between the text and icon
  },

});
//***********************************************************************************/

/**
 * This function will navigate user to the selected screen
 * depening either he is gues or admin
 * @param {*} navigation - stack of screens so that function can keep track of current screen 
 * @returns - JSX Element
 */
export default function Home({ navigation }) {
  /**
 * This function will be invoked, when user clicks on the Guest button
 * This will navigate the user to Guest screen
 */
  const handleGuestPress = () => {
    // Handle Guest button click
    navigation.navigate('Guest')
  };

  /**
   * This function will be invoked, when user clicks on the Admin button
   * This will navigate the user to Admin  screen
   */
  const handleAdminPress = () => {
    // Handle Admin button click
    navigation.navigate('Login')

  };


  return (
    <BackgroundImage source={{ uri: 'https://media.giphy.com/media/BHNfhgU63qrks/giphy.gif' }} resizeMode="cover">
      <View style={styles.headView}>
        <Text style={styles.headingText}>SVM</Text>
      </View>
      <View style={styles.mainView}>
        <Button onPress={handleGuestPress}>
          <ButtonText>Guest</ButtonText>
        </Button>
        <Button onPress={handleAdminPress}>
          <ButtonText>Admin</ButtonText>
        </Button>
      </View>
    </BackgroundImage>
  );
};