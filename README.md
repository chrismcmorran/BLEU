# BLEU

## What is this?
This is an implementation of "BLEU: a Method for Automatic Evaluation of Machine Translation" as described by Kishore Papineni, Salim Roukos, Todd Ward, and Wei-Jing Zhu. The paper can be found here: https://dl.acm.org/doi/10.3115/1073083.1073135

In short, BLEU provides a way to determine how "good" a machine translation is using a set of reference sentences and a modified precision metric. As with all academic work, the set of equations underline below make little to no sense without the following context being provided. The variable `n` is usually set to 4, which this library does by default. The variable `r`refers to a reference set, `c` refers to a machine translation, or "candidate" sentence, `w` refers to a set of weights, which we default to `1`. These weights can be parameterized with calls to `Score` in the library. The log in the scoring equation uses Euler's constant as a base.

<a href="https://www.codecogs.com/eqnedit.php?latex=p_{n}&space;=&space;\dfrac{\sum_{C&space;\in&space;Candidates}&space;\sum_{n-gram&space;\in&space;C}&space;Count_{Clip}(n-gram)}{\sum_{C^{\prime}&space;\in&space;Candidates}&space;\sum_{n-gram&space;\in&space;C^{\prime}}&space;Count(n-gram^{\prime})}" target="_blank"><img src="https://latex.codecogs.com/gif.latex?p_{n}&space;=&space;\dfrac{\sum_{C&space;\in&space;Candidates}&space;\sum_{n-gram&space;\in&space;C}&space;Count_{Clip}(n-gram)}{\sum_{C^{\prime}&space;\in&space;Candidates}&space;\sum_{n-gram&space;\in&space;C^{\prime}}&space;Count(n-gram^{\prime})}" title="p_{n} = \dfrac{\sum_{C \in Candidates} \sum_{n-gram \in C} Count_{Clip}(n-gram)}{\sum_{C^{\prime} \in Candidates} \sum_{n-gram \in C^{\prime}} Count(n-gram^{\prime})}" /></a>


<a href="https://www.codecogs.com/eqnedit.php?latex=BP&space;=\begin{cases}&space;1&&space;c&space;>&space;r\\e^{(1&space;-&space;r/c)}&&space;c&space;\leq&space;r\end{cases}" target="_blank"><img src="https://latex.codecogs.com/gif.latex?BP&space;=\begin{cases}&space;1&&space;c&space;>&space;r\\e^{(1&space;-&space;r/c)}&&space;c&space;\leq&space;r\end{cases}" title="BP =\begin{cases} 1& c > r\\e^{(1 - r/c)}& c \leq r\end{cases}" /></a>

<a href="https://www.codecogs.com/eqnedit.php?latex=BLEU&space;=&space;BP&space;\cdot&space;exp\left(\sum_{n=1}^{N}&space;w_{n}\log{p_{n}}\right)" target="_blank"><img src="https://latex.codecogs.com/gif.latex?BLEU&space;=&space;BP&space;\cdot&space;exp\left(\sum_{n=1}^{N}&space;w_{n}\log{p_{n}}\right)" title="BLEU = BP \cdot exp\left(\sum_{n=1}^{N} w_{n}\log{p_{n}}\right)" /></a>

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
