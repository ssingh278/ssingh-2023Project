//***********************************************************************************
//Program: Mobile App for Smart Vending Machine
//Description:  This file is the Main file that will be called by index.js to display
//              everything on the mobile screen  
//Date: 10-Oct -2023
//Author: Sharry Singh
//***********************************************************************************


//***********************************************************************************
// All Imports go here
import React, { useState } from 'react'

import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';


import Home from './Screens/Home';
import Guest from './Screens/Guest';
import Loading from './Screens/Loading';
import AdminLogin from './Screens/AdminLogin';
import AdminDashboard from './Screens/AdminDashboard';

//***********************************************************************************

const Stack = createNativeStackNavigator(); //variable that will maintain the Stack of screens



/**
 * this function will be displayed in the Mobile App , this is the main
 * screen, and all other features will be add on to this App function,
 * This function have stack of all the screens that will be used in the 
 * mobile app
 * @returns JSX.Element - this will be displayed on Index.html
 */
export default function App() {
    return (
        <NavigationContainer>
            <Stack.Navigator>
                <Stack.Screen
                    name="Loading"
                    component={Loading}
                    options={{ title: 'Loading', headerShown: false }}
                />
                <Stack.Screen
                    name="Home"
                    component={Home}
                    options={{ title: 'Home', headerShown: false }}
                />
                <Stack.Screen
                    name="Guest"
                    component={Guest}
                    options={{ title: 'Guest', headerShown: false }}
                />

                <Stack.Screen
                    name="Login"
                    component={AdminLogin}
                    options={{ title: 'LoginScreen', headerShown: false }}
                />

                <Stack.Screen
                    name="AdminDashboard"
                    component={AdminDashboard}
                    options={{ title: 'AdminDashboard', headerShown: false }}
                />



            </Stack.Navigator>
        </NavigationContainer>
    )
}