import React from "react";
import { Link } from "react-router-dom";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ArrowRightAltIcon from "@material-ui/icons/ArrowRightAlt";

export default function Component(props: any) 
{

  return (
    <section>
      <Container maxWidth="md">
        <Box py={8} textAlign="center">
          <Typography variant="overline" component="span">Welcome to my web page</Typography>
          <Typography variant="h4" component="h2">Hello, my name is Tomasz but I usually go by Tom and I do programming for a living...</Typography>
          <Box mt={4}>
            <Link to="/mystory">
              <Button color="primary" endIcon={<ArrowRightAltIcon />}>Read more</Button>         
            </Link>
          </Box>
        </Box>
      </Container>
    </section>
  );

}
