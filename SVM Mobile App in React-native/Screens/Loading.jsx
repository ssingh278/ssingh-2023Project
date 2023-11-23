//***********************************************************************************
//Program: Mobile App for Smart Vending Machine
//Description:  This file create the function that will contain an animation
//              that will be showed when App is launch  
//Date: 10-Oct -2023
//Author: Sharry Singh
//***********************************************************************************

//***********************************************************************************
// All import goes here
import { StyleSheet, Text, View, ImageBackground } from 'react-native'
import React, { useState, useEffect } from 'react'
import { styled } from 'styled-components/native';
//***********************************************************************************

/**
 * This is my own defined tag that will used as an body element to cover all mobile screen
 * to display the giphy . it has some defined styling 
 */
const BackgroundImage = styled(ImageBackground)`
  flex: 1;
  justify-content: center;
  align-items: center;
`;

/**
 * This function will display the giphy for 4 seconds 
 * and then will navigate the user to Home screen
 * @param {*} navigation - stack of navigation for app to keep track of current scrren
 * @returns nothing
 */
export default function Loading({ navigation }) {
  // Hide the Giphy image after 4 seconds
  useEffect(() => {
    const timer = setTimeout(() => {
      navigation.navigate('Home');
    }, 4000); // 4 seconds in milliseconds

    return () => {
      clearTimeout(timer);
    };
  }, []);
  return (
    <BackgroundImage source={{ uri: 'https://c.tenor.com/FJQJzNAuTqkAAAAC/loading-microsoft-windows.gif' }} resizeMode="cover"></BackgroundImage>
  )
}
