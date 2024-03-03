import React, { useState, useEffect } from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Menu, MenuItem } from '@mui/material';
import { TranslationJob, Translator } from '../../types';
import axios from 'axios';

export const TranslatorJobTable: React.FC<{ formData: (data: any) => void }> = ({ formData }) => {
  const [translationJobs, setTranslationJobs] = useState<TranslationJob[]>([]);
  const [anchorElStatus, setAnchorElStatus] = useState<null | HTMLElement>(null);
  const [anchorElTranslator, setAnchorElTranslator] = useState<null | HTMLElement>(null);
  const [selectedJob, setSelectedJob] = useState<TranslationJob | null>(null);
  const [translators, setTranslators] = useState<Translator[]>([]);

  useEffect(() => {
    fetchData();
    fetchTranslators();
  }, [formData]);

  const fetchData = async () => {
    try {
      const response = await axios.get('http://localhost:7729/api/TranslationJob');
      const data = response.data;
      setTranslationJobs(data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const fetchTranslators = async () => {
    try {
      const response = await axios.get('http://localhost:7729/api/TranslatorManagement');
      const data = response.data;
      setTranslators(data);
    } catch (error) {
      console.error('Error fetching translators:', error);
    }
  };

  const handleClickStatus = (event: React.MouseEvent<HTMLButtonElement>, job: TranslationJob) => {
    setAnchorElStatus(event.currentTarget);
    setSelectedJob(job);
  };

  const handleClickTranslator = (event: React.MouseEvent<HTMLButtonElement>, job: TranslationJob) => {
    setAnchorElTranslator(event.currentTarget);
    setSelectedJob(job);
  };

  const handleCloseStatus = () => {
    setAnchorElStatus(null);
  };

  const handleCloseTranslator = () => {
    setAnchorElTranslator(null);
  };

  const handleStatusChange = async (status: number) => {
    if (selectedJob) {  
    try {  
        await axios.put(`http://localhost:7729/api/TranslationJob/UpdateJobStatus?jobId=${selectedJob.id}&status=${status}`);
        fetchData();
      } catch (error: any) {
        alert(error.response.data);
      }
      handleCloseStatus();
  }};

  const handleTranslatorSelect = async (translatorId: number) => {
    if (selectedJob) {
      try {
        await axios.post(`http://localhost:7729/api/TranslatorManagement/AssignTranslator?translatorId=${translatorId}&jobId=${selectedJob.id}`);
        fetchData();
      } catch (error: any) {
        alert(error.response.data);
      }
    }
    handleCloseTranslator();
  };

  return (
    <div>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Customer Name</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Price</TableCell>
              <TableCell>Translator Name</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {translationJobs.map(translationJob => (
              <TableRow key={translationJob.id}>
                <TableCell>{translationJob.id}</TableCell>
                <TableCell>{translationJob.customerName}</TableCell>
                <TableCell>
                  <Button aria-controls="status-menu" aria-haspopup="true" onClick={(event) => handleClickStatus(event, translationJob)}>
                    {translationJob.status}
                  </Button>
                  <Menu
                    id="status-menu"
                    anchorEl={anchorElStatus}
                    keepMounted
                    open={Boolean(anchorElStatus)}
                    onClose={handleCloseStatus}
                  >
                    <MenuItem onClick={() => handleStatusChange(0)}>New</MenuItem>
                    <MenuItem onClick={() => handleStatusChange(1)}>InProgress</MenuItem>
                    <MenuItem onClick={() => handleStatusChange(2)}>Completed</MenuItem>
                  </Menu>
                </TableCell>
                <TableCell>{translationJob.price}</TableCell>
                <TableCell>
                  <Button aria-controls="translator-menu" aria-haspopup="true" onClick={(event) => handleClickTranslator(event, translationJob)}>
                    {translationJob.translator === null? "NOT ASSIGNED": translationJob.translator.name}
                  </Button>
                  <Menu
                    id="translator-menu"
                    anchorEl={anchorElTranslator}
                    keepMounted
                    open={Boolean(anchorElTranslator)}
                    onClose={handleCloseTranslator}
                  >
                    {translators.map(translator => (
                      <MenuItem key={translator.id} onClick={() => handleTranslatorSelect(translator.id)}>
                        {translator.name}
                      </MenuItem>
                    ))}
                  </Menu>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default TranslatorJobTable;