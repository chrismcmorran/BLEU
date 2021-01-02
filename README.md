# BLEU

## What is this?
This is an implementation of "BLEU: a Method for Automatic Evaluation of Machine Translation" as described by Kishore Papineni, Salim Roukos, Todd Ward, and Wei-Jing Zhu. The paper can be found here: https://dl.acm.org/doi/10.3115/1073083.1073135

## Why bother?
Many Open Source implementations are available, such as the one found in NLTK. However, I did not find one written in C# and figured that it would be helpful to myself and many other developers who are doing Machine Learning in C# to have an easy to use implementation readily available.

## Examples

### Absolute BLEU Score
```
var bleu = new BleuScore();
var reference = new List<string> {"the quick brown fox jumped over the lazy dog"};
var candidate = "the fast brown fox jumped over the lazy dog";
var score = bleu.Score(reference, candidate);
```
### A rounded BLEU score (to 2 decimal places)
```
var bleu = new BleuScore();
var reference = new List<string> {"the quick brown fox jumped over the lazy dog"};
var candidate = "the fast brown fox jumped over the lazy dog";
var score = bleu.RoundedScore(reference, candidate);
Assert.AreEqual(0.75, score);
```
### Collecting `n-grams`
```
var testString = "this is a test string";
var expected = new []{"this is a", "is a test", "a test string"};
var collector = new NGramCollector(testString, 3);
var result = collector.Collect();
Assert.AreEqual(expected, result);
```
