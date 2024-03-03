import React, { useState } from 'react';
import {TextField, Button, MenuItem, FormControl, InputLabel } from '@mui/material';
import Select, { SelectChangeEvent } from '@mui/material/Select';
import {TranslatorStatus, TranslatorStatusOptions} from '../../types';
import axios from 'axios';


export const TranslatorForm : React.FC<{ updateTable: (data: any) => void }> = ({ updateTable }) => {
  const [formData, setFormData] = useState({
    id:0,
    name: '',
    hourlyRate: '',
    status: 0,
    creditCardNumber: ''
  });  

  const [status, setStatus] = useState<TranslatorStatus>(
    TranslatorStatus.Applicant
  );
  

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  function changeStatus(event: SelectChangeEvent){
    setFormData({
        ...formData,
        status: parseInt(event.target.value),
      });
    setStatus(parseInt(event.target.value))
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
    const options = {
            method: 'POST',
            url: 'http://localhost:7729/api/TranslatorManagement',
            headers: {
                'Content-Type': 'application/json'
            },
            data: formData
        };

        const response = await axios.request(options);
      alert('Translator created successfully!');
      setFormData({
        id:0,
        name: '',
        hourlyRate: '',
        status: 0,
        creditCardNumber: ''
      });
      updateTable(formData);
    } catch (error) {
      console.error('Error creating translator:', error);
      alert('Error creating translator. Please try again.');
    }
  };

  return (
    <form style={{ margin: '0px 50px' }} onSubmit={handleSubmit}>    
      <TextField
        name="name"
        label="Name"
        value={formData.name}
        onChange={handleChange}
        fullWidth
        margin="normal"
        variant="outlined"
        required
      />
      <TextField
        name="hourlyRate"
        label="Hourly Rate"
        value={formData.hourlyRate}
        onChange={handleChange}
        fullWidth
        margin="normal"
        variant="outlined"
      />
      <FormControl fullWidth margin="normal">
        <InputLabel>Status</InputLabel>
        <Select
          name="status"
          value= {TranslatorStatusOptions[status].id.toString()}
          onChange={changeStatus}
          label="Status">
          {TranslatorStatusOptions.map((option: any) => (
            <MenuItem key={option.id} value={option.id}>{option.name}</MenuItem>
          ))}
        </Select>
      </FormControl>
      <TextField
        name="creditCardNumber"
        label="Credit Card Number"
        value={formData.creditCardNumber}
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

export default TranslatorForm;
