import React from "react";
import Grid from "@material-ui/core/Grid";

export default function Structure(props: { column1: any; column2: any; }) 
{
  
  const columns = 
  {
    '1': (Array.isArray(props.column1) ? props.column1 : []),
    '2': (Array.isArray(props.column2) ? props.column2 : [])
  }

  return (
    <Grid container spacing={2}>
      <Grid item xs={12} md={6}>
        {columns['1'].map((component: React.ReactNode) => <React.Fragment>{component}</React.Fragment>)} 
      </Grid>
      <Grid item xs={12} md={6}>
        {columns['2'].map((component: React.ReactNode) => <React.Fragment>{component}</React.Fragment>)} 
      </Grid>
    </Grid>
  );

}
