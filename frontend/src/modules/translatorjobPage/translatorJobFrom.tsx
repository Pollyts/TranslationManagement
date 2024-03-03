import React, { useState } from 'react';
import {TextField, Button, MenuItem, FormControl, InputLabel } from '@mui/material';
import axios from 'axios';


export const TranslatorJobForm : React.FC<{ updateTable: (data: any) => void }> = ({ updateTable }) => {
  const [formData, setFormData] = useState({
    id:0,
    customerName: '',
    originalContent: '',
  });    

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(formData)
    try {
    const options = {
            method: 'POST',
            url: 'http://localhost:7729/api/TranslationJob',
            headers: {
                'Content-Type': 'application/json'
            },
            data: formData
        };
        const response = await axios.request(options);
        if (response.status === 201){
          alert("Successfully created")
        }
        else{
          alert(response.data)
        }
        setFormData({
        id:0,
        customerName: '',
        originalContent: '',
      });
      updateTable(formData);
    } catch (error: any) {
      alert(error.response.data);
    }
  };

  return (
    <form style={{ margin: '0px 50px' }} onSubmit={handleSubmit}>    
      <TextField
        name="customerName"
        label="customerName"
        value={formData.customerName}
        onChange={handleChange}
        fullWidth
        margin="normal"
        variant="outlined"
        required
      />
      <TextField
        name="originalContent"
        label="originalContent"
        value={formData.originalContent}
        onChange={handleChange}
        fullWidth
        margin="normal"
        variant="outlined"
      />      
      <Button type="submit" variant="contained" color="primary">
        Create
      </Button>
    </form>
  );
};

export default TranslatorJobForm;
