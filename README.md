# DVLD - إدارة رخص السواقين

مشروع مكتبي باستخدام Visual Studio لإدارة رخص القيادة (https://drive.google.com/file/d/1620_1PUySLtKK7vyEGAjhL0QGfzVXx3_/view?usp=drivesdk)

## المحتوى:
- ✅ كود المشروع (Forms + DAL + BLL)
- 🖼️ صور محفوظة في مجلد: DVLD-People-Images
- 🗄️ قاعدة البيانات: DVLD_SchemaAndData.sql

## طريقة التشغيل:
1. افتح DVLD.sln باستخدام Visual Studio.
2. نفّذ ملف SQL الموجود داخل DVLD_SchemaAndData.sql لإنشاء قاعدة البيانات.
3. تأكد من وجود الصور في المسار التالي:

> 📌 ملاحظة مهمة: يجب نسخ مجلد الصور DVLD-People-Images ووضعه في القرص C، لأن البرنامج يستخدم هذا المسار لتحميل الصور.
و من أجل تشغيل المشروع اسم المستخدم GhaderUser و كلمة المرور 4321
---

## ملاحظات إضافية

- تم تجاهل الملفات المؤقتة (مثل bin و obj) تلقائيًا في .gitignore
- يمكن تعديل مسار الصور من الكود في حال رغبت بوضع المجلد داخل المشروع
