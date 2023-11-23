import React, { useState, useEffect } from 'react';
import { View, TextInput, Button, Text, StyleSheet, Alert } from 'react-native';

const AdminLogin = ({ navigation }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const sampleUsername = 'Admin';
  const samplePassword = 'Password';

  const handleLogin = () => {
    if (email === sampleUsername && password === samplePassword) {
      setError('');
      setSuccess('Login successful!');
      navigation.navigate('AdminDashboard')

      // You can add authentication logic here, e.g., navigating to the main screen
    } else {
      setError('Incorrect username or password. Please try again.');
    }
  };

  const handleInputChange = () => {
    setError('');
    setSuccess('');
  };

  useEffect(() => {
    const unsubscribe = navigation.addListener('blur', () => {
      // Clear the email and password fields when navigating away from the login page
      setEmail('');
      setPassword('');
      setError('');
      setSuccess('');
    });

    return unsubscribe;
  }, [navigation]);

  return (
    <View style={styles.container}>
      <Text style={styles.heading}>Admin Login</Text>
      <Text style={styles.errorText}>{error}</Text>
      <Text style={styles.successText}>{success}</Text>
      <View style={styles.formContainer}>
        <TextInput
          style={styles.input}
          placeholder="Email"
          onChangeText={setEmail}
          value={email}
          onTextInput={handleInputChange}
        />
        <TextInput
          style={styles.input}
          placeholder="Password"
          onChangeText={setPassword}
          value={password}
          secureTextEntry={true}
          onTextInput={handleInputChange}
        />
        <Button title="Login" onPress={handleLogin} />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'flex-start',
    paddingHorizontal: 20,
    backgroundColor: '#8e44ad',
  },
  heading: {
    fontSize: 36,
    fontWeight: 'bold',
    color: 'white',
    textShadowColor: 'black',
    textShadowOffset: { width: 2, height: 2 },
    textShadowRadius: 5,
    textAlign: 'center',
    marginBottom: 20,
  },
  formContainer: {
    flex: 1,
    justifyContent: 'center',
  },
  errorText: {
    color: 'red',
    fontSize: 16,
    textAlign: 'center',
    marginBottom: 10,
  },
  successText: {
    color: 'green',
    fontSize: 16,
    textAlign: 'center',
    marginBottom: 10,
  },
  input: {
    height: 40,
    borderColor: 'gray',
    borderWidth: 1,
    marginBottom: 20,
    paddingHorizontal: 10,
    backgroundColor: 'white',
    borderRadius: 20,
  },
});

export default AdminLogin;
