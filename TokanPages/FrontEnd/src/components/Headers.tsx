import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ArrowRightAltIcon from "@material-ui/icons/ArrowRightAlt";

const useStyles = makeStyles((theme) => ({}));

export default function Component(props: any) 
{

  const classes = useStyles();

  return (
    <section>
      <Container maxWidth="md">
        <Box py={8} textAlign="center">
          <Typography variant="overline" component="span">Welcome to my web page</Typography>
          <Typography variant="h4" component="h2">Hello, my name is Tomasz but I usually go by Tom and I do programming for a living...</Typography>
          <Box mt={4}>
            <Button color="primary" endIcon={<ArrowRightAltIcon />} href="/mystory">Read more</Button>
          </Box>
        </Box>
      </Container>
    </section>
  );

}
