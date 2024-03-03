import React, { useState } from 'react';
import { Button, Box, Grid } from '@mui/material';
import './App.css';
import { TranslatorPage } from '../src/modules/translatorPage/translatorPage';
import { TranslatorJobPage } from '../src/modules/translatorjobPage/translatorjobPage';

function App() {
  const [activeTab, setActiveTab] = useState<'TranslatorPage' | 'TranslatorJobPage'>('TranslatorJobPage');

  const handleTabChange = (tab: 'TranslatorPage' | 'TranslatorJobPage') => {
    setActiveTab(tab);
  };

  return (
    <div className="App">
      <Box marginTop={2} marginBottom={2}>
        <Grid container justifyContent="center" spacing={2}>
          <Grid item>
            <Button variant={activeTab === 'TranslatorPage' ? 'contained' : 'outlined'} onClick={() => handleTabChange('TranslatorPage')}>Translator</Button>
          </Grid>
          <Grid item>
            <Button variant={activeTab === 'TranslatorJobPage' ? 'contained' : 'outlined'} onClick={() => handleTabChange('TranslatorJobPage')}>Translator Jobs</Button>
          </Grid>
        </Grid>
      </Box>
      {activeTab === 'TranslatorPage' && <TranslatorPage />}
      {activeTab === 'TranslatorJobPage' && <TranslatorJobPage />}
    </div>
  );
}

export default App;