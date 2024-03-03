import React, { useState, useEffect } from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button, Menu, MenuItem } from '@mui/material';
import { Translator } from '../../types';

export const TranslatorTable: React.FC<{ formData: (data: any) => void }> = ({ formData }) => {
  const [translators, setTranslators] = useState<Translator[]>([]);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [selectedTranslator, setSelectedTranslator] = useState<Translator | null>(null);

  useEffect(() => {
    fetchData();
  }, [formData]);

  const fetchData = async () => {
    try {
      const response = await fetch('http://localhost:7729/api/TranslatorManagement');
      const data = await response.json();
      setTranslators(data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const handleClick = (event: React.MouseEvent<HTMLButtonElement>, translator: Translator) => {
    setAnchorEl(event.currentTarget);
    setSelectedTranslator(translator);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleStatusChange = async (status: number) => {
    if (selectedTranslator) {
      try {
        await fetch(`http://localhost:7729/api/TranslatorManagement/UpdateStatus?translatorId=${selectedTranslator.id}&status=${status}`, {
          method: 'PUT'
        });
        fetchData();
      } catch (error) {
        console.error('Error updating status:', error);
      }
    }
    handleClose();
  };

  return (
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Name</TableCell>
              <TableCell>Hourly Rate</TableCell>
              <TableCell>Status</TableCell>
              <TableCell>Credit Card Number</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {translators.map(translator => (
              <TableRow key={translator.id}>
                <TableCell>{translator.id}</TableCell>
                <TableCell>{translator.name}</TableCell>
                <TableCell>{translator.hourlyRate}</TableCell>
                <TableCell>
                  <Button aria-controls="status-menu" aria-haspopup="true" onClick={(event) => handleClick(event, translator)}>
                    {translator.status === 0 ? 'Applicant' : translator.status === 1 ? 'Certified' : 'Deleted'}
                  </Button>
                  <Menu
                    id="status-menu"
                    anchorEl={anchorEl}
                    keepMounted
                    open={Boolean(anchorEl)}
                    onClose={handleClose}
                  >
                    <MenuItem onClick={() => handleStatusChange(0)}>Applicant</MenuItem>
                    <MenuItem onClick={() => handleStatusChange(1)}>Certified</MenuItem>
                    <MenuItem onClick={() => handleStatusChange(2)}>Deleted</MenuItem>
                  </Menu>
                </TableCell>
                <TableCell>{translator.creditCardNumber}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
  )
};

export default TranslatorTable;