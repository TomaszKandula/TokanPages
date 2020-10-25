import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Box from "@material-ui/core/Box";
import Typography from "@material-ui/core/Typography";

const useStyles = makeStyles((theme) => (
{

  block: 
  {
    marginBottom: theme.spacing(3),
  },
  image: 
  {
    maxWidth: "100%",
  }

}));

export default function Content(props: { content: any; }) 
{

  const classes = useStyles();

  const content = 
  {
    "title": "Why strong tea isn't good for CEOs",
    "date": "November 14, by Dinesh Chugtai",
    "paragraph1": "Laudantium, unde aliquam sit accusantium a explicabo maiores doloribus aut, rerum accusamus alias saepe molestias ut suscipit voluptate voluptatibus repellendus fuga vero. Error delectus odit, numquam laborum consectetur mollitia corrupti quo neque, quibusdam tempore debitis voluptatum vitae! Ea explicabo totam excepturi! Eius?",
    "paragraph2": "Alias sunt voluptas ratione modi dolore nostrum debitis nihil. Nemo, ratione repellat quia doloremque perferendis fuga cumque ex corporis laborum distinctio dolorum deserunt voluptates ea architecto ab, esse omnis quas provident. Maiores sed ipsam eos quis.",
    "subtitle": "An exhaustive guide about how different teas can affect a CEO during their workday.",
    "paragraph3": "Necessitatibus porro suscipit consequatur, eum, odio rem sequi quisquam, libero fuga qui mollitia ullam temporibus. Repudiandae eum vitae iste odit esse, eligendi ipsum, aut ipsam provident unde quidem aperiam ad maiores et, illum corrupti incidunt quasi. Ipsa sint assumenda cupiditate molestiae vitae et rerum non eum suscipit tempore.",
    "paragraph4": "Alias sunt voluptas ratione modi dolore nostrum debitis nihil. Nemo, ratione repellat quia doloremque perferendis fuga cumque ex corporis laborum distinctio dolorum deserunt voluptates ea architecto ab, esse omnis quas provident. Maiores sed ipsam eos quis.",
    "image": "https://images.unsplash.com/photo-1521012012373-6a85bade18da?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1351&q=80",
    ...props.content
  };

  return (
    <section>
      <Container maxWidth="sm">
        
        <Box py={8}>
          
          <Box mb={4} textAlign="center">
            <Typography variant="overline" component="span">{content['date']}</Typography>
            <Typography variant="h3" component="h2">{content['title']}</Typography>
          </Box>
          
          <Box>
            <Typography variant="body1" color="textSecondary" paragraph={true} className={classes.block}>{content['paragraph1']}</Typography>

            <Typography variant="body1" color="textSecondary" paragraph={true} className={classes.block}>{content['paragraph2']}</Typography>

            <Box mt={4} mb={3}>
              <Typography variant="h5">{content['subtitle']}</Typography>
            </Box>

            <Typography variant="body1" color="textSecondary" paragraph={true} className={classes.block}>{content['paragraph3']}</Typography>

            <Box my={4}>
              <img src={content['image']} alt="" className={classes.image} />
            </Box>

            <Typography variant="body1" color="textSecondary" paragraph={true} className={classes.block}>{content['paragraph4']}</Typography>
          </Box>
        
        </Box>
      </Container>
    </section>
  );
}
