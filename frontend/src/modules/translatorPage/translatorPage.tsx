import React, { useState, useEffect } from 'react';
import { Grid } from '@mui/material';
import { TranslatorForm } from '../translatorPage/translatorFrom';
import { TranslatorTable } from '../translatorPage/translatorTable';

export const TranslatorPage: React.FC = () => { 

  const [formData, setFormData] = useState<any>(null);

  const handleFormChange = (newData: any) => {
    setFormData(newData);
  };

  return (
    <div>      
      <Grid container spacing={2}>        
        <Grid item xs={4}>
        <h1>Create Translator</h1>
          <TranslatorForm updateTable={handleFormChange}/>
        </Grid>
        <Grid item xs={8} >          
          <h1>Translators</h1>
          <TranslatorTable formData={formData}/>
        </Grid>
      </Grid>
    </div>
  );
};

export default TranslatorPage;