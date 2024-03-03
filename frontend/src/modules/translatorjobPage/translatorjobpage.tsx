import React, { useState, useEffect } from 'react';
import { Grid } from '@mui/material';
import { TranslationJob, Translator } from '../../types';
import { TranslatorJobForm } from './translatorJobFrom';
import { TranslatorJobTable } from './translatorJobTable';

export const TranslatorJobPage: React.FC = () => {
  const [formData, setFormData] = useState<any>(null);

  const handleFormChange = (newData: any) => {
    setFormData(newData);
  };

  return (
    <div>      
      <Grid container spacing={2}>        
        <Grid item xs={4}>
        <h1>Create translation job</h1>
          <TranslatorJobForm updateTable={handleFormChange}/>
        </Grid>
        <Grid item xs={8} >          
          <h1>Translator jobs</h1>
          <TranslatorJobTable formData={formData}/>
        </Grid>
      </Grid>
    </div>
  );
};

export default TranslatorJobPage;