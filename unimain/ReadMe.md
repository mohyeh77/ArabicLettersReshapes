<!-- Look at these-->
# Arabic Letters Reshape
---
<!--![](./assets/img/mym2024.png)-->

<img src="./assets/img/mym2024.png" alt="Logo" width="300" style="display: block; margin: auto;" />


<!--<div>
<span style="font-size: 3.2em; font-weight: bold;">Mohamed Yehia</span><br>
</div>-->

# Introduction
___
We fully recognize that the Arabic language has a distinctive approach to handling letters. Each character in Arabic can appear in four contextual forms: isolated, initial, medial, and final. This behavior is closely tied to the direction of writing—Arabic is written from right to left, unlike Latin-based scripts.

During composition, letters connect dynamically based on their position within a word. Each letter has specific joining rules:  
- Some connect from both the right and left sides  
- Some connect from only one side  
- Others do not connect at all  
___
Let’s consider a practical example:

## Example

<span style="font-size: 1.2em; font-weight: bold;">
**The word “Muhammad”**  <br>
Basic characters: م ح م د  <br>
Contextual forms: مـ - حـ - مـ - د  <br>
</span><br>

## The Challenge
To address the complexity of rendering Arabic script correctly, we rely on the Unicode standard—a unified coding system that assigns a unique identifier to each letter form (glyph). This allows for precise manipulation and display of Arabic text across digital platforms.

## The Solution
I’ve developed a software library that automatically converts basic Arabic characters into their correct Unicode representations, based on their position within a word. This enables developers to render Arabic text either as visually connected script or as a sequence of Unicode values suitable for programming purposes.
## Usage
The code is free to use and continuously updated. While it’s open-source, credit is due to its original author.  
And if I may ask—remember me in your prayers. May God envelop us in His infinite mercy.

---

# Coming Soon
- A GUI application for easy text conversion.
- A Ligature converter for standard ligatures.
