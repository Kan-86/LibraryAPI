/**
 * Responds to any HTTP request.
 *
 * @param {!express:Request} req HTTP request context.
 * @param {!express:Response} res HTTP response context.
 */
const translate = require('@google-cloud/translate')();

exports.helloWorld = (req, res) => {
  let msg = req.query.msg || 'Hello, world!';
  let lang = req.query.lang || 'es';
  translate.translate(msg, lang, (err, translation) => {
    if (err) console.error(err);
    res.status(200).send(translation);
  });
};