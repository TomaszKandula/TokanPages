import React from "react";
import Container from "@material-ui/core/Container";

export default function Structure(props: { columns: any; }) 
{
  const columns = 
  {
    '1': (Array.isArray(props.columns) ? props.columns : [])
  }

  return (
    <Container>
      {columns['1'].map(component => <React.Fragment>{component}</React.Fragment>)} 
    </Container>
  );

}
